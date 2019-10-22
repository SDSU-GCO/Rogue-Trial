using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CollidedWithPlayer : ConditionalComponent
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public override bool Result(Collider2D otherCollider)
    {
        if (crossSceneDataSO.playerTransform != null)
            return otherCollider.gameObject == crossSceneDataSO.playerTransform.gameObject;
        else
            return false;
    }

    public override bool Result(Collision2D collision)
    {
        if (crossSceneDataSO.playerTransform != null)
            return collision.otherCollider.gameObject == crossSceneDataSO.playerTransform.gameObject;
        else
            return false;
    }
}
