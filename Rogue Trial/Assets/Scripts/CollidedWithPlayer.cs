using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CollidedWithPlayer : ConditionalComponent
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    GameObject player;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public override bool Result(Collider2D otherCollider)
    {
        return otherCollider.gameObject == player;
    }

    public override bool Result(Collision2D collision)
    {
        Debug.Log(collision.otherCollider.gameObject);
        return collision.otherCollider.gameObject == player;
    }

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        Debug.Log(otherCollider.gameObject);
        if (otherCollider.gameObject == player)
        Debug.Log(otherCollider.gameObject);
    }
}
