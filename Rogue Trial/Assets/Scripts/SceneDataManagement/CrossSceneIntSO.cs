using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneIntSO", menuName = "ScriptableObjects/CrossSceneIntSO")]
public class CrossSceneIntSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    int _value;
    public int Value
    {
        get => _value;
        set { if (_value != value) /*then*/ { _value = value; /*and*/ Event?.Invoke(); } }
    }
}
