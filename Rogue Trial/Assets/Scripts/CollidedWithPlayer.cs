using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CollidedWithPlayer : ConditionalComponent
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneTransformSO playerTransformSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public override bool Result(Collider2D otherCollider) => 
        playerTransformSO.value != null ? otherCollider.gameObject == playerTransformSO.value.gameObject : false;

}
