using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
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

    [ProgressBar("Jump", 0.5f, ProgressBarColor.Blue), ShowIf("showJumpBar")]
    public float airbornTime = 0;

    private bool showJumpBar() => !isGrounded;
    public UnityEvent Jumped;

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
    public UnityEvent Dashed;


    bool jumpButtonPressed;



    [SerializeField, ReadOnly]
    private bool isGrounded = true;
    [SerializeField, ReadOnly]
    private bool isDashing = false;

    public bool IsGrounded => isGrounded;
    public bool IsDashing => isDashing;

    private void OnValidate()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if (capsuleCollider2D == null)
        {
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private Vector2 velocity = new Vector2();

    private void Awake()
    {
        if(enableDebugging)
        {
            circleCastEdge.GetComponent<SpriteRenderer>().enabled = true;
            circleCastOrigin.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            circleCastEdge.GetComponent<SpriteRenderer>().enabled = false;
            circleCastOrigin.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    //update loop
    private void Update()
    {
        jumpButtonPressed = Input.GetAxis("Vertical") > float.Epsilon;
        isGrounded = CheckGrounded();

        if (isGrounded)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }

        if (isGrounded)
        {
            airbornTime = 0;
        }
        if (!isGrounded && jumpButtonPressed!=true)
        {
            airbornTime = allowedAirbornTime;
        }

        //Debug.Log(Input.GetAxis("Vertical"));
        //run
        velocity = rigidbody2D.velocity;
        velocity.x = Input.GetAxis("Horizontal") * runSpeed;
        rigidbody2D.velocity = velocity;

        //jump
        if (jumpButtonPressed && airbornTime <= allowedAirbornTime)
        {
            velocity = rigidbody2D.velocity;
            velocity.y = jumpVelocity;
            rigidbody2D.velocity = velocity;
            ////non linear jump
            //rigidbody2D.AddForce(Vector2.up * jumpVelocity);
            Jumped.Invoke();
        }

        //smart gravity
        if(isGrounded!=true)
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

        airbornTime = Mathf.Min(airbornTime + Time.deltaTime, allowedAirbornTime + 1);

        //dash
        if(Input.GetAxis("Dash") > float.Epsilon && dash == 0 && dashUsed!=true && isDashing==false)
        {
            isDashing = true;
            StartCoroutine(Dash());
            Dashed.Invoke();
        }

        dash = Mathf.Max(dash - Time.deltaTime, 0);
        dashUsed = isGrounded ? false : dashUsed;
    }

    int groundLayers = (1 << (int)CustomGCOTypes.CollisionLayerKey.Ground) | (1 << (int)CustomGCOTypes.CollisionLayerKey.Platform);
    private bool CheckGrounded()
    {
        bool result = false;
        LayerMask layerMask = groundLayers;
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(layerMask);

        RaycastHit2D raycastHit2D;
        //capsule cast
        raycastHit2D = Physics2D.CapsuleCast(((Vector2)transform.position) + capsuleCollider2D.offset, capsuleCollider2D.size, capsuleCollider2D.direction, 0, Vector2.down, 1, layerMask);
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
        float forceDirection = (spriteRenderer.flipY ? (-1) : 1);
        while (dashProgress < dashTime)
        {
            rigidbody2D.AddForce(Vector2.right * forceDirection * dashForce);
            dashProgress += Time.deltaTime;
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