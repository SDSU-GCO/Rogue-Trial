using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Movement : MonoBehaviour
{
    public float runSpeed = 3;
    public float jumpVelocity = 3;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2;
    public float allowedAirbornTime = .5f;
    public float airbornTime = 0;
    public UnityEvent Jumped;

    [SerializeField, HideInInspector]
    private new Rigidbody2D rigidbody2D = null;
    [SerializeField, HideInInspector]
    private CapsuleCollider2D capsuleCollider2D = null;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    //[ReadOnly]
    public bool isGrounded = true;
    bool jumpButtonPressed;

    public float dashCooldown = .5f;
    public float dashForce = 100;
    public float dashTime = 0.5f;
    private float dash = 0;
    private bool dashUsed = false;

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

    int groundLayers;
    private void Awake()
    {
        groundLayers = (1<<LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"));
    }

    //update loop
    private void Update()
    {
        jumpButtonPressed = Input.GetAxis("Vertical") > float.Epsilon;
        isGrounded = CheckGrounded();

        //if (isGrounded)
        //{
        //    spriteRenderer.color = Color.green;
        //}
        //else
        //{
        //    spriteRenderer.color = Color.white;
        //}

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
            Jumped.Invoke();
        }

        //smart gravity
        if (rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity += Vector2.up * (-9.8f) * (fallMultiplier) * Time.deltaTime;
        }
        else if (rigidbody2D.velocity.y > 0 && jumpButtonPressed!=true)
        {
            rigidbody2D.velocity += Vector2.up * (-9.8f) * (lowJumpMultiplier) * Time.deltaTime;
        }

        airbornTime = Mathf.Min(airbornTime + Time.deltaTime, allowedAirbornTime + 1);

        if(Input.GetAxis("Dash") > float.Epsilon && dash == 0 && dashUsed!=true)
        {
            StartCoroutine(Dash());
        }
        dash = Mathf.Max(dash - Time.deltaTime, 0);
        dashUsed = isGrounded ? false : dashUsed;
    }

    private bool CheckGrounded()
    {
        bool result = false;
        LayerMask layerMask = groundLayers;
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(layerMask);

        RaycastHit2D raycastHit2D;
        raycastHit2D = Physics2D.CapsuleCast(transform.position, capsuleCollider2D.size, capsuleCollider2D.direction, 0, Vector2.down, 1, layerMask);

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
        float dashProgress = 0;
        float forceDirection = (spriteRenderer.flipY ? (-1) : 1);
        while (dashProgress < dashTime)
        {
            rigidbody2D.AddForce(Vector2.right * forceDirection * dashForce);
            dashProgress += Time.deltaTime;
            yield return null;
        }
    }
}