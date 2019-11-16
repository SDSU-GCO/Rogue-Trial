using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyList : ConditionalComponent
{
    [SerializeField]
    [HideInInspector]
    public EnemyListMBDO enemyListMBDO;

    

    private void OnValidate()
    {

        if (enemyListMBDO == null)
        {
            MBDOInitializationHelper mBDOInitializationHelper = default;

            //IMPORTNANT STEP!!!
            mBDOInitializationHelper.SetupCardinalSubSystem(this);
            mBDOInitializationHelper.SetupMBDO(ref enemyListMBDO);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
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
