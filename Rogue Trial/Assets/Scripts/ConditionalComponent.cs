using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalComponent : MonoBehaviour
{
    public abstract bool Result(Collision2D collision);
    public abstract bool Result(Collider2D collision);
    CollisionTrigger[] collisionTriggers;

    private void Awake()
    {
        collisionTriggers = GetComponents<CollisionTrigger>();
    }

    private void OnEnable()
    {
        foreach (CollisionTrigger collisionTrigger in collisionTriggers)
        {
            if (collisionTrigger.conditionalComponents.Contains(this)!=true)
            {
                collisionTrigger.conditionalComponents.Add(this);
            }
        }
    }
    private void OnDisable()
    {
        foreach (CollisionTrigger collisionTrigger in collisionTriggers)
        {
            if (collisionTrigger.conditionalComponents.Contains(this))
            {
                collisionTrigger.conditionalComponents.Remove(this);
            }
        }
    }
}
