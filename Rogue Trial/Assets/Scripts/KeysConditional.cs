using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KeysConditional : ConditionalComponent
{
#pragma warning disable CS0649
    [SerializeField] [HideInInspector] KeyListMBDO keyListMBDO;
    [SerializeField, Required]
    CrossSceneBoolSO crossSceneBoolSO;
#pragma warning restore CS0649
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (keyListMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref keyListMBDO);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    public override bool Result(Collider2D c)
    {
        crossSceneBoolSO.Value = (keyListMBDO.keys.Count == 0);
        return keyListMBDO.keys.Count == 0;
    }
}
