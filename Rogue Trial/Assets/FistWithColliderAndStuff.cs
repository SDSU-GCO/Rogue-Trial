using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class FistWithColliderAndStuff : MonoBehaviour
{
    public UnityAction myFunc;
    bool collidedWithPlayer = false;

    [SerializeField]
    Collider2D[] cols;

    public void ActCol()
    {
        foreach (Collider2D c in cols)
            c.enabled = true;
    }

    public void DeactCol()
    {
        foreach (Collider2D c in cols)
            c.enabled = false;
    }

    public void PunchDone()
    {
        if(!collidedWithPlayer)
        {
            myFunc.Invoke();
        }
        collidedWithPlayer = false;

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name=="Player")
        {
            Entity_Logic playerE = collision.gameObject.GetComponent<Entity_Logic>();
            playerE.TakeDamage(4);
            collidedWithPlayer = true;
        }
    }
}
