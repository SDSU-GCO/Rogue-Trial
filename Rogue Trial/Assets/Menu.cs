using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameStateSO gameStateSO;
    private void OnValidate()
    {
        if(gameStateSO==null)
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
