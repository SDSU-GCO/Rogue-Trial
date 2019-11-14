using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Player_Attack_Logic : MonoBehaviour
{
    [SerializeField]
    private Attack_Controller rangedAttack = null;

    [SerializeField, HideInInspector]
    private float rangedCoolDownInSeconds = 0;

    [SerializeField, HideInInspector]
    private float rangedCoolDownInSecondsDefault;

    [SerializeField, HideInInspector]
    private int damage;

    [SerializeField, HideInInspector]
    public SpriteRenderer spriteRenderer;

    [SerializeField, HideInInspector]
    private new Rigidbody2D rigidbody2D;

    [SerializeField, HideInInspector]
    FlipSpriteOnVelocity flipSpriteOnVelocity;

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
        if (Input.GetMouseButton(1))
        {
            PlayerRangedAttack();
        }
    }

    //shoot player projectile
    private void PlayerRangedAttack()
    {
        if (rangedCoolDownInSeconds == 0)
        {
            if (flipSpriteOnVelocity)
                flipSpriteOnVelocity.forceLookRight = spriteRenderer.flipX;

            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            Vector2 mouseposition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            mouseposition = (mouseposition - (Vector2)transform.position).normalized * offset;

            GameObject childInstance = Instantiate(rangedAttack.gameObject, mouseposition + (Vector2)transform.position, transform.rotation);

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
            Debug.Log(spriteRenderer.flipX);
            
        }
    }
}