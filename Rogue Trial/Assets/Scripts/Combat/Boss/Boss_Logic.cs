using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private float bossAttackCooldown;
    private float bossAttackCooldownDefault;

    [SerializeField, HideInInspector]
    private Entity_Logic entityLogic;

    [SerializeField]
    private Attack_Controller rangedAttack = null;

    private void InitializeFromRangedAttack()
    {
        if (rangedAttack != null)
        {
            bossAttackCooldown = 0;
            bossAttackCooldownDefault = rangedAttack.AttackDelay;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        //OnValidate();
        if (entityLogic == null)
        {
            entityLogic = GetComponent<Entity_Logic>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossAttackCooldown = Mathf.Max(0, bossAttackCooldown - Time.deltaTime);

        //if attacks are off cooldown

        //randomly assign an attack not self damaging one

        //execute attack

        //every three regular attacks, do self damaging one

    }

    private void punchPlayer()
    {
        //punch off screen

        //locate player

        //place fist above the sector the player is in

        //fist slams down

        //stall for half a second before retreating
    }

    private void rockRain()
    {
        //all the attack logic
        bossAttackCooldown = bossAttackCooldownDefault;
    }

    private void punchCeiling()
    {
        //all the attack logic
        bossAttackCooldown = bossAttackCooldownDefault;

        //hurt itself in its confusion
        gameObject.GetComponent<Entity_Logic>().health--; 
    }
}
