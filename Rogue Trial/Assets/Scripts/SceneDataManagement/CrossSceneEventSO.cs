using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneEventSO", menuName = "ScriptableObjects/CrossSceneEventSO")]
public class CrossSceneEventSO : ScriptableObject
{
    public QuickEvent Event;
}
