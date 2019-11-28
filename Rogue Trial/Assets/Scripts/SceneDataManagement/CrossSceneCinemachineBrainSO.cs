
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneCinemachineBrainSO", menuName = "ScriptableObjects/CrossSceneCinemachineBrainSO")]
public class CrossSceneCinemachineBrainSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    Cinemachine.CinemachineBrain _value;
    public Cinemachine.CinemachineBrain Value
    {
        get => _value;
        set { if (_value != value) /*then*/ { _value = value; /*and*/ Event?.Invoke(); } }
    }
}

