using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BossAttack : MonoBehaviour
{
    [SerializeField, BoxGroup("Prefabs")]
    private Attack_Controller rangedAttack = null;

    [SerializeField, BoxGroup("Component Refs")]
    public SpriteRenderer spriteRenderer;
#pragma warning disable CS0109
    [SerializeField, BoxGroup("Component Refs")]
    private new Rigidbody2D rigidbody2D;
#pragma warning restore CS0109

    [SerializeField, HideInInspector]
    private float rangedCoolDownInSecondsDefault;
    [SerializeField, HideInInspector]
    private int damage;

    private bool CheckRangedAttackNotNull() => rangedAttack != null;



    GameObject childInstance = null;

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
    }

        // Update is called once per frame
        void Update()
    {
        
    }

    private void InitializeFromRangedAttack()
    {
        if (rangedAttack != null)
        {
            rangedCoolDownInSecondsDefault = rangedAttack.AttackDelay;
            damage = rangedAttack.damage;
        }
    }

    void SpawnPunch()
    {

        childInstance = Instantiate(rangedAttack.gameObject, rigidbody2D.transform.position, transform.rotation);
    }

}
