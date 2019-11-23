using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] int amount;
    float currentCoolDown = 0f;
    [SerializeField]
    float coolDownInSeconds = 0.25f;
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
