using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    KeyListMBDO keyListMBDO;

    private void OnValidate()
    {
        if(Application.isEditor)
        {
            if(keyListMBDO==null)
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

    private void OnEnable()
    {
        if (!(keyListMBDO.keys.Contains(this)))
            keyListMBDO.keys.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keyListMBDO.keys.Contains(this))
            keyListMBDO.keys.Remove(this);
    }

    private void OnDisable()
    {
        if (keyListMBDO.keys.Contains(this))
            keyListMBDO.keys.Remove(this);
    }
}
