using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField, HideInInspector]
    GameStateSO gameStateSO;
#pragma warning restore CS0649
    private void OnValidate()
    {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            if (gameStateSO == null)
            {
                gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
            }
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    private void OnEnable()
    {
        if (gameStateSO.MenuOpen != true)
        {
            gameStateSO.MenuOpen = true;
        }
    }
    private void OnDisable()
    {
        if (gameStateSO.MenuOpen == true)
        {
            gameStateSO.MenuOpen = false;
        }
    }
}
