using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using ByteSheep.Events;

public class ClearCrossSceneData : MonoBehaviour
{
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO = null;
    [SerializeField, Required]
    Transform playerTransform = null;


    // Start is called before the first frame update
    void Awake()
    {
        crossSceneDataSO.combat = false;
        crossSceneDataSO.platformer = false;
        crossSceneDataSO.keysRoom = false;
        crossSceneDataSO.playerTransform = playerTransform;
        

    }
    
}
