using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    KeyListMBDO keyListMBDO;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            if(keyListMBDO==null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;


                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref keyListMBDO);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }

    private void OnEnable()
    {
        if (!(keyListMBDO.keys.Contains(this)))
            keyListMBDO.keys.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (keyListMBDO.keys.Contains(this))
            keyListMBDO.keys.Remove(this);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (keyListMBDO.keys.Contains(this))
            keyListMBDO.keys.Remove(this);
    }
}
