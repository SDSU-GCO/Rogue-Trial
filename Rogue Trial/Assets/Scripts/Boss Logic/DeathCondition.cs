using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;


[RequireComponent(typeof(Health))]

public class DeathCondition : MonoBehaviour
{
    public QuickEvent OnDeath;
    private Health HP;
    public Animator BossAnimator;
    public MonoBehaviour BossBrain;

    private void Awake()
    {
        HP = GetComponent<Health>();
    }

    private void Start()
    {
        HP.EmptyEvent.AddListener(DeathAction);
    }

    private void DeathAction()
    {
        BossAnimator.SetBool("isAlive", false);
        BossBrain.enabled = false;
    }
}


