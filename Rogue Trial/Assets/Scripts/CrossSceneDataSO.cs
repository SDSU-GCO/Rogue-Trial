using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CrossSceneDataSO", menuName = "ScriptableObjects/CrossSceneDataSO")]
public class CrossSceneDataSO : ScriptableObject
{
    public bool combat = false;
    public bool platformer = false;
    public bool keysRoom = false;
}
