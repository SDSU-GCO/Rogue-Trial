using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttackSpawner : MonoBehaviour
{
    [SerializeField]
    Transform cornerBL;
    [SerializeField]
    Transform cornerUR;
    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject WinFadeObj;

    [SerializeField]
    BossBrain bossBrain;

    [SerializeField]
    Health health;

   
    Vector2 GetPosition()
    {
        float horizontal = 0;
        if(player.position.x<cornerBL.position.x)
        {
            horizontal = cornerBL.position.x;
        }
        else if(player.position.x>cornerUR.position.x)
        {
            horizontal = cornerUR.position.x;
        }
        else
        {
            horizontal = player.position.x;
        }
        float vertical = cornerUR.position.y;

        Vector2 rtn = default;
        rtn.x = horizontal;
        rtn.y = vertical;
        return rtn;
    }

    public void GenerateObject()
    {
        Vector3 position = GetPosition();

        position.z = -9;

        GameObject fist = Instantiate(prefab, position, transform.rotation);
        position = fist.transform.localPosition;
        position.y = 1;
        fist.transform.localPosition = position;

        FistWithColliderAndStuff fistWithColliderAndStuff = fist.GetComponent<FistWithColliderAndStuff>();
        fistWithColliderAndStuff.myFunc = TakeDamage;
    }

    void TakeDamage()
    {
        health.CurrentHealth -= 1;
        if (health.CurrentHealth <= 0)
            Die();
    }

    void Die()
    {
        bossBrain.enabled = false;
        if(WinFadeObj!=null)
        WinFadeObj.SetActive(true);
    }
}
