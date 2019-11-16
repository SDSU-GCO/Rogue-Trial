using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using ByteSheep.Events;

public class ManageCrossSceneData : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneBoolSO[] roomClearData;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value


    // Start is called before the first frame update
    void Awake()
    {
        foreach (CrossSceneBoolSO csb in roomClearData)
        {
            csb.value = false;
        }
    }
    
}
