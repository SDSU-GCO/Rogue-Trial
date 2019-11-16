using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using ByteSheep.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour, IMovable, IUsesInput
{
    [SerializeField, Required, BoxGroup("Component Refs")]
    private new Rigidbody2D rigidbody2D = null;
    [SerializeField, Required, BoxGroup("Component Refs")]
    private CapsuleCollider2D capsuleCollider2D = null;
    [SerializeField, Required, BoxGroup("Component Refs")]
    private SpriteRenderer spriteRenderer = null;

    [MinValue(0)]
    public float runSpeed = 3;
    [MinValue(0), BoxGroup("Jump vars")]
    public float jumpVelocity = 3;
    [MinValue(0), BoxGroup("Jump vars")]
    public float fallMultiplier = 2.5f;
    [MinValue(0), BoxGroup("Jump vars")]
    public float lowJumpMultiplier = 2;
    [MinValue(0), BoxGroup("Jump vars")]
    public float allowedAirbornTime = .5f;

    [ProgressBar("Jump", 0.5f, ProgressBarColor.Blue), ShowIf("ShowJumpBar")]
    public float airbornTime = 0;

    private bool ShowJumpBar() => !isGrounded;

    [SerializeField, BoxGroup("Jump vars")]
    public QuickEvent Jumped = new QuickEvent();

    [MinValue(0), BoxGroup("Dash vars")]
    public float dashCooldown = .5f;
    [MinValue(0), BoxGroup("Dash vars")]
    public float dashForce = 100;
    [MinValue(0), BoxGroup("Dash vars")]
    public float dashTime = 0.5f;
    private bool dashUsed = false;

    [ProgressBar("DashCooldown", 0.5f, ProgressBarColor.Green), ShowIf("isDashing")]
    public float dash = 0;
    [ProgressBar("Dash", 0.5f, ProgressBarColor.Green), ShowIf("isDashing")]
    public float dashProgress = 0;
    [BoxGroup("Dash vars")]
    public QuickEvent Dashed;


    [SerializeField, BoxGroup("Constraints")]
    bool enableInputs = true;
    public bool EnableInputs
    {
        get => enableInputs;
        set => enableInputs = value;
    }

    [SerializeField, BoxGroup("Constraints")]
    CustomGCOTypes.MovementState movementState = CustomGCOTypes.MovementState.Enabled;
    public CustomGCOTypes.MovementState MovementState
    {
        get
        {
            return movementState;
        }
        set
        {
            if (value == CustomGCOTypes.MovementState.Disabled)
            {
                //cache momentum
                cachedVelocity = rigidbody2D.velocity;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else if (value == CustomGCOTypes.MovementState.DisabledKillMomentum)
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                cachedVelocity = Vector2.zero;
            }
            else if (value == CustomGCOTypes.MovementState.Enabled)
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigidbody2D.velocity = cachedVelocity;
            }
            else
            {
                Debug.LogError(value + " is not implemented yet!");
            }
            movementState = value;
        }
    }
    Vector2 cachedVelocity = Vector2.zero;

    [SerializeField, ReadOnly]
    private bool isGrounded = true;
    [SerializeField, ReadOnly]
    private bool isDashing = false;

    public bool IsGrounded => isGrounded;
    public bool IsDashing => isDashing;

    bool jumpButtonPressed;

    [SerializeField, HideInInspector]
    PlayerTransformMBDO playerTransformMBDO;

    //private void OnValidate()
    //{
    //    if (rigidbody2D == null)
    //    {
    //        rigidbody2D = GetComponent<Rigidbody2D>();
    //    }

    //    if (capsuleCollider2D == null)
    //    {
    //        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    //    }

    //    if (spriteRenderer == null)
    //    {
    //        spriteRenderer = GetComponent<SpriteRenderer>();
    //    }

    //    Debug.Log("OnValidate: " + this + " scene: " + gameObject.scene.name);
    //    if (playerTransformMBDO == null)
    //    {
    //        MBDOInitializationHelper mBDOInitializationHelper = default;

    //        //IMPORTNANT STEP!!!
    //        mBDOInitializationHelper.SetupCardinalSubSystem(this);
    //        mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
    //        if(playerTransformMBDO!=null && playerTransformMBDO.playerTransform==null)
    //        {
    //            Debug.LogWarning("Assignment to playerTransformMBDO.playerTransform in: " + this);
    //            playerTransformMBDO.playerTransform = transform;
    //        }
    //    }
    //}
    //private void Reset()
    //{
    //    Debug.Log("OnReset: " + this);
    //    if (playerTransformMBDO == null)
    //    {
    //        MBDOInitializationHelper mBDOInitializationHelper = default;

    //        //IMPORTNANT STEP!!!
    //        mBDOInitializationHelper.SetupCardinalSubSystem(this);
    //        mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
    //        if (playerTransformMBDO != null && playerTransformMBDO.playerTransform == null)
    //        {
    //            Debug.LogWarning("Assignment to playerTransformMBDO.playerTransform in: " + this);
    //            playerTransformMBDO.playerTransform = transform;
    //        }
    //    }
    //}

    private void Awake()
    {
        if (circleCastEdge != null)
            circleCastEdge.GetComponent<SpriteRenderer>().enabled = enableDebugging;

        if (circleCastOrigin != null)
            circleCastOrigin.GetComponent<SpriteRenderer>().enabled = enableDebugging;
    }

    //update loop
    private void Update()
    {
        if(movementState == CustomGCOTypes.MovementState.Enabled)
        {
            {//handle inputs
                jumpButtonPressed = Input.GetAxis("Vertical") > float.Epsilon;

                //run
                Vector2 velocity = rigidbody2D.velocity;
                velocity.x = Input.GetAxis("Horizontal") * runSpeed;
                rigidbody2D.velocity = velocity;

                //dash
                if (Input.GetAxis("Dash") > float.Epsilon && dash == 0 && dashUsed != true && isDashing == false)
                {
                    isDashing = true;
                    StartCoroutine(Dash());
                    Dashed.Invoke();
                }

                //jump
                if (jumpButtonPressed && airbornTime < allowedAirbornTime)
                {
                    velocity = rigidbody2D.velocity;
                    velocity.y = jumpVelocity;
                    rigidbody2D.velocity = velocity;
                    ////non linear jump
                    //rigidbody2D.AddForce(Vector2.up * jumpVelocity);
                    Jumped.Invoke();
                }
            }

            {//update physics
                isGrounded = CheckGrounded();
                //smart gravity
                if (isGrounded != true)
                {
                    if (rigidbody2D.velocity.y < 0)
                    {
                        rigidbody2D.velocity += Vector2.up * (-9.8f) * (fallMultiplier) * Time.deltaTime;
                    }
                    else if (rigidbody2D.velocity.y > 0 && jumpButtonPressed != true)
                    {
                        rigidbody2D.velocity += Vector2.up * (-9.8f) * (lowJumpMultiplier) * Time.deltaTime;
                    }
                }

                //update dash timer
                dash = Mathf.Max(dash - Time.deltaTime, 0);
                dashUsed = isGrounded ? false : dashUsed;

                //update airborn timer
                airbornTime = Mathf.Min(airbornTime + Time.deltaTime, allowedAirbornTime);

                //update jump constraints
                if (isGrounded)//reset the jump on the ground
                    airbornTime = 0;
                else if (jumpButtonPressed != true)//if we release the jump button off the ground, don't allow a resume jump
                    airbornTime = allowedAirbornTime;
            }
        }
        if (enableDebugging == true)//change the color while on the ground
        {
            spriteRenderer.color = isGrounded ? Color.green : Color.white;
        }
    }

    readonly int groundLayers = (1 << (int)CustomGCOTypes.CollisionLayerKey.Ground) | (1 << (int)CustomGCOTypes.CollisionLayerKey.Platform);
    private bool CheckGrounded()
    {
        bool result = false;
        LayerMask layerMask = groundLayers;
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(layerMask);

        RaycastHit2D raycastHit2D;
        //capsule cast
        //raycastHit2D = Physics2D.CapsuleCast(((Vector2)transform.position) + capsuleCollider2D.offset, capsuleCollider2D.size, capsuleCollider2D.direction, 0, Vector2.down, 1, layerMask);
        //circle cast
        Vector2 circleOffset = capsuleCollider2D.size.y > capsuleCollider2D.size.x ?  capsuleCollider2D.offset + Vector2.down * ((capsuleCollider2D.size.y - capsuleCollider2D.size.x)/2.0f) : capsuleCollider2D.offset;
        Vector2 circleOrigin = (Vector2)transform.position + circleOffset;
        Vector2 circleRadius = (circleOrigin + Vector2.down * capsuleCollider2D.size.x * 0.5f);
        if (circleCastOrigin != null && enableDebugging==true)
            circleCastOrigin.position = new Vector3(circleOrigin.x, circleOrigin.y, circleCastOrigin.position.z);
        if (circleCastEdge != null && enableDebugging==true)
            circleCastEdge.position = new Vector3(circleRadius.x, circleRadius.y, circleCastOrigin.position.z);
        raycastHit2D = Physics2D.CircleCast(circleOrigin, capsuleCollider2D.size.x*0.5f, Vector2.down, Mathf.Infinity, layerMask);

        if (raycastHit2D.collider != null && raycastHit2D.distance > 0 && raycastHit2D.distance < 0.1)
        {
            result = true;
        }

        return result;
    }

    IEnumerator Dash()
    {
        dash = dashCooldown;
        dashUsed = true;
        dashProgress = 0;
        float forceDirection = (spriteRenderer.flipX ? (-1) : 1);
        while (dashProgress < dashTime)
        {
            if (movementState==CustomGCOTypes.MovementState.Enabled)
            {
                rigidbody2D.AddForce(Vector2.right * forceDirection * dashForce);
                dashProgress += Time.deltaTime;
            }
            yield return null;
        }
        isDashing = false;
    }
    [SerializeField, BoxGroup("Debug Disp Widget Objects")]
    private bool enableDebugging = false;
    [BoxGroup("Debug Disp Widget Objects"), ShowIf("enableDebugging")]
    public Transform circleCastOrigin = null;
    [BoxGroup("Debug Disp Widget Objects"), ShowIf("enableDebugging")]
    public Transform circleCastEdge = null;

}
