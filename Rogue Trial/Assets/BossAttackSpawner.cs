using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ByteSheep.Events;

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


#pragma warning disable CS0649
    [SerializeField]
    QuickEvent TakeDamage = new QuickEvent();
    UnityAction myact;
#pragma warning restore CS0649


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
        myact = Ouch;
        fistWithColliderAndStuff.myFunc = myact;
    }

    void Ouch()
    {
        TakeDamage.Invoke();
    }

    void Die()
    {
        bossBrain.enabled = false;
        if(WinFadeObj!=null)
        WinFadeObj.SetActive(true);
    }
}
