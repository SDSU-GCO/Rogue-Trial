﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CollidedWithPlayer : ConditionalComponent
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    PlayerTransformMBDO playerTransformMBDO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (playerTransformMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;

                //IMPORTNANT STEP!!!
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            if (playerTransformMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;

                //IMPORTNANT STEP!!!
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
            }
        }
    }
    public override bool Result(Collider2D otherCollider) =>
        playerTransformMBDO.playerTransform != null ? otherCollider.gameObject == playerTransformMBDO.playerTransform.gameObject : false;

}
