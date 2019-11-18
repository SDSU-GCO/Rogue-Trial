using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Player_Attack_Logic : MonoBehaviour
{
    [SerializeField, BoxGroup("Prefabs")]
    private Attack_Controller rangedAttack = null;

    [SerializeField, HideInInspector]
    private float rangedCoolDownInSeconds = 0;

    [SerializeField, HideInInspector]
    private float rangedCoolDownInSecondsDefault;

    [SerializeField, HideInInspector]
    private int damage;

    [SerializeField, BoxGroup("Component Refs")]
    public SpriteRenderer spriteRenderer;

    [SerializeField, BoxGroup("Component Refs")]
    private new Rigidbody2D rigidbody2D;

    [SerializeField, BoxGroup("Component Refs")]
    FlipSpriteOnVelocity flipSpriteOnVelocity;

    [SerializeField, BoxGroup("Component Refs")]
    PlayerMovement playerMovement;

    public float offset = 1.5f;

    private bool CheckRangedAttackNotNull() => rangedAttack != null;

    private void Awake()
    {
        InitializeFromRangedAttack();

        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (flipSpriteOnVelocity == null)
            flipSpriteOnVelocity = GetComponent<FlipSpriteOnVelocity>();
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    private void InitializeFromRangedAttack()
    {
        if (rangedAttack != null)
        {
            rangedCoolDownInSecondsDefault = rangedAttack.AttackDelay;
            damage = rangedAttack.damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rangedCoolDownInSeconds = Mathf.Max(0, rangedCoolDownInSeconds - Time.deltaTime);
        if (Input.GetMouseButtonDown(1))
        {
            PlayerRangedAttack();
        }
    }

    //shoot player projectile
    private void PlayerRangedAttack()
    {
        if (rangedCoolDownInSeconds == 0)
        {
            if (flipSpriteOnVelocity!=null)
                flipSpriteOnVelocity.forceLookRight = !spriteRenderer.flipX;

            ////original attack pos code
            //Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //Vector2 mouseposition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            //mouseposition = (mouseposition - (Vector2)transform.position).normalized * offset;
            //GameObject childInstance = Instantiate(rangedAttack.gameObject, mouseposition + (Vector2)transform.position, transform.rotation);

            ////discussed pos attack code
            //GameObject childInstance = null;
            //if (spriteRenderer.flipX != true)
            //    childInstance = Instantiate(rangedAttack.gameObject, Vector2.right * offset + (Vector2)transform.position, transform.rotation);
            //else
            //    childInstance = Instantiate(rangedAttack.gameObject, Vector2.left * offset + (Vector2)transform.position, transform.rotation);
            //playerMovement.MovementState = CustomGCOTypes.MovementState.Disabled;

            ////proposed attack pos code ver 1
            //GameObject childInstance = null;
            //Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //Vector2 mouseposition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            //Vector2 normalPos = (mouseposition - (Vector2)transform.position).normalized;

            //if (spriteRenderer.flipX != true && normalPos.x < 0)
            //    normalPos.x = normalPos.x * -1;
            //else if (spriteRenderer.flipX == true && normalPos.x > 0)
            //    normalPos.x = normalPos.x * -1;

            //mouseposition = normalPos * offset;
            //childInstance = Instantiate(rangedAttack.gameObject, mouseposition + (Vector2)transform.position, transform.rotation);


            //proposed attack pos code ver 2
            GameObject childInstance = null;
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector2 mouseposition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 normalPos = (mouseposition - (Vector2)transform.position).normalized;

            if (spriteRenderer != null && flipSpriteOnVelocity != null)
            {
                if (normalPos.x < 0)
                {
                    spriteRenderer.flipX = true;
                    flipSpriteOnVelocity.forceLookRight = false;
                }
                else
                {
                    spriteRenderer.flipX = false;
                    flipSpriteOnVelocity.forceLookRight = true;
                }
            }

            mouseposition = normalPos * offset;
            childInstance = Instantiate(rangedAttack.gameObject, mouseposition + (Vector2)transform.position, transform.rotation);


            Vector3 temp = childInstance.transform.position;
            temp.z = transform.position.z;
            childInstance.transform.position = temp;
            //childInstance.GetComponent<Rigidbody2D>().velocity = rangedAttack.speed * mouseposition.normalized;

            childInstance.transform.parent = transform;

            rangedCoolDownInSeconds = rangedCoolDownInSecondsDefault;


            SpriteRenderer childSprite = childInstance.GetComponent<SpriteRenderer>();
            if (childSprite != null)
            {
                childSprite.flipX = spriteRenderer.flipX;
            }
            Attack_Controller childAttack_Controller = childInstance.GetComponent<Attack_Controller>();
            if (childAttack_Controller != null)
            {
                childAttack_Controller.whenDestroyed = ResetForceLook;
            }
            //Debug.Log(spriteRenderer.flipX);

        }
    }
    void ResetForceLook()
    {

        playerMovement.MovementState = CustomGCOTypes.MovementState.Enabled;
        if (flipSpriteOnVelocity != null)
            flipSpriteOnVelocity.forceLookRight = null;
    }
}