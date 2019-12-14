using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

public class BossBrain : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField]
    QuickEvent weakAttack = new QuickEvent();
    [SerializeField]
    QuickEvent strongAttack = new QuickEvent();
    [SerializeField, NaughtyAttributes.Required]
    Animator animator;
#pragma warning restore CS0649

    [SerializeField]
    int weakAttackCount = 5;
    [SerializeField]
    int weakAttackCountIncremeantAmount = 2;
    [SerializeField]
    float attackDelay = 2;
    [SerializeField]
    float attackDelayDecrementAmount = 0.5f;
    [SerializeField]
    float idleTime = 4f;

    void NextPhase()
    {
        weakAttackCount += weakAttackCountIncremeantAmount;
        attackDelay -= attackDelayDecrementAmount;
    }

    float timer3 = 0;

    bool isIdle = false;
    public void SetIdle() => isIdle = true;
    public void ClearIdle() => isIdle = false;
    private void Update()
    {
        
        timer3 = Mathf.Min(idleTime, timer3 + Time.deltaTime);
        if (isIdle && (timer3 == idleTime))
        {
            StartAttackSequence();
            timer3 = 0;
        }
    }

    public void StartAttackSequence()
    {
        animator.SetBool("Attack", true);
    }
    [SerializeField]
    float aditionalWindupDelay = 0;
    public void WindupAnimationComplete()
    {
        Debug.Log("LEL");
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        bool attacking = true;
        float timer = 0;
        int attacksInCurrentSequence = 0;

        while (attacking)
        {
            timer = Mathf.Min(aditionalWindupDelay, timer + Time.deltaTime);
            if (timer == aditionalWindupDelay)
                attacking = false;
            yield return null;
        }

        attacking = true;
        timer = 0;
        while (attacking)
        {
            Debug.Log("HEY");
            timer = Mathf.Min(attackDelay, timer + Time.deltaTime);
            if (timer == attackDelay && attacking)
            {
                timer = 0;

                if (attacksInCurrentSequence < weakAttackCount)
                {
                    Debug.Log("WEAK");
                    weakAttack.Invoke();
                    attacksInCurrentSequence++;
                }
                else
                {
                    Debug.Log("So much strength");
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
