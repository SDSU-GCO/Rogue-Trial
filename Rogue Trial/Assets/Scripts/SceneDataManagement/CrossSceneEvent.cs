using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneEventSO", menuName = "ScriptableObjects/CrossSceneEventSO")]
public class CrossSceneEvent : ScriptableObject
{
    public QuickEvent SomeEvent;
    private void Awake()
    {
        SomeEvent.RemoveAllListeners();
    }
}
