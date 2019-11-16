using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField, HideInInspector]
    GameStateSO gameStateSO;
    private void OnValidate()
    {
        if(gameStateSO==null)
        {
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    private void OnEnable()
    {
        if (gameStateSO.MenuOpen != true)
        {
            gameStateSO.MenuOpen = true;
            gameStateSO.updatedValue.Invoke();
        }
    }
    private void OnDisable()
    {
        if (gameStateSO.MenuOpen == true)
        {
            gameStateSO.MenuOpen = false;
            gameStateSO.updatedValue.Invoke();
        }
    }
}
