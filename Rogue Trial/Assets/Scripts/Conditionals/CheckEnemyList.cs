using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyList : ConditionalComponent
{
#pragma warning disable CS0649
    [SerializeField, HideInInspector]
    EnemyListMBDO enemyListMBDO;
#pragma warning restore CS0649

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {

            if (enemyListMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;

                //IMPORTNANT STEP!!!
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref enemyListMBDO);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }


    public override bool Result(Collider2D dafuqIsThis)
    {
        return UnlockDoor();
    }

    public bool UnlockDoor()
    {
        
        bool unlockDoor = false;
        if (enemyListMBDO.enemies.Count == 0)
            unlockDoor = true;
        else
        {
            unlockDoor = false;
        }
        return unlockDoor;
    }
}
