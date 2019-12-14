using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneBool", menuName = "ScriptableObjects/CrossSceneBool")]
public class CrossSceneBoolSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    [SerializeField]
    bool _value;
    public bool Value
    {
        get => _value;
        set { if (_value != value) /*then*/ { _value = value; /*and*/ Event?.Invoke(); }}
    }
}
