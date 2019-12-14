using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField] Health health;
    [SerializeField] int amount;
    float currentCoolDown = 0f;
    [SerializeField]
    float coolDownInSeconds = 0.25f;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public void HealEntity()
    {
        if (currentCoolDown == coolDownInSeconds)
        {
            health.CurrentHealth += amount;
            currentCoolDown = 0f;
        }
    }

    private void Update()
    {
        currentCoolDown = Mathf.Min(currentCoolDown + Time.deltaTime, coolDownInSeconds);
    }
}
