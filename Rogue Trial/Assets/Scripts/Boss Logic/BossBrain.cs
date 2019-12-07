using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

public class BossBrain : MonoBehaviour
{
    [SerializeField]
    QuickEvent weakAttack = new QuickEvent();
    [SerializeField]
    QuickEvent strongAttack = new QuickEvent();
    [SerializeField, NaughtyAttributes.Required]
    Animator animator;

    [SerializeField]
    int weakAttackCount = 5;
    [SerializeField]
    int weakAttackCountIncremeantAmount = 2;
    [SerializeField]
    float attackDelay = 2;
    [SerializeField]
    float attackDelayDecrementAmount = 0.5f;

    void NextPhase()
    {
        weakAttackCount += weakAttackCountIncremeantAmount;
        attackDelay -= attackDelayDecrementAmount;
    }

    public void StartAttackSequence()
    {
        animator.SetBool("Attack", true);
    }
    [SerializeField]
    float aditionalWindupDelay = 0;
    public void WindupAnimationComplete()
    {
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        bool attacking = true;
        float timer = 0;
        int attacksInCurrentSequence = 0;

        while (attacking)
        {
            timer = Mathf.Min(attackDelay, timer + Time.deltaTime);
            if (timer == aditionalWindupDelay)
                attacking = false;
        }


        attacking = true;
        timer = 0;
        while (attacking)
        {
            timer = Mathf.Min(attackDelay, timer + Time.deltaTime);
            if (timer == attackDelay && attacking)
            {
                timer = 0;

                if (attacksInCurrentSequence < weakAttackCount)
                {
                    weakAttack.Invoke();
                    attacksInCurrentSequence++;
                }
                else
                {
                    strongAttack.Invoke();
                    attacksInCurrentSequence = 0;
                    NextPhase();
                    attacking = false;
                }
            }
            yield return null;
        }

        animator.SetInteger("HP", animator.GetInteger("HP")-1);
        animator.SetBool("Attack", false);
    }
}
