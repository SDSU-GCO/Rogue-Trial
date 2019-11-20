using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] int amount;
    public void HealEntity()
    {
        health.CurrentHealth += amount;
    }
}
