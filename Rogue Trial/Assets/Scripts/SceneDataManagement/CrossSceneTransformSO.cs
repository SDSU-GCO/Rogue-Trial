using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneTransformSO", menuName = "ScriptableObjects/CrossSceneTransformSO")]
public class CrossSceneTransformSO : ScriptableObject
{
    Transform _value;
    public QuickEvent Event = new QuickEvent();

    public Transform Value
    {
        get => _value;
        set { if (_value != value) /*then*/ { _value = value; /*and*/ Event?.Invoke(); } }
    }
}
