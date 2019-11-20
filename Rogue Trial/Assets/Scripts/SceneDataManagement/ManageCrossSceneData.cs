using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using ByteSheep.Events;

public class ManageCrossSceneData : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneBoolSO[] roomClearData;
    [SerializeField]
    int CurrentPlayerHPDefault;
    [SerializeField]
    CrossSceneIntSO CurrentPlayerHP;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value


    // Start is called before the first frame update
    void Awake()
    {
        ResetGame();
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            if(CurrentPlayerHP!=null)
            {
                CurrentPlayerHP.value = CurrentPlayerHPDefault;
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.EditorUtility.SetDirty(CurrentPlayerHP);
            }
            else
            {
                Debug.LogError("CurrentPlayerHP is null in " + this);
            }
        }
#endif
    }

    public void ResetGame()
    {
        foreach (CrossSceneBoolSO csb in roomClearData)
        {
            csb.value = false;
        }

        if (CurrentPlayerHP != null)
        {
            CurrentPlayerHP.value = CurrentPlayerHPDefault;
        }
        else
        {
            Debug.LogError("CurrentPlayerHP is null in " + this);
        }
    }

}
