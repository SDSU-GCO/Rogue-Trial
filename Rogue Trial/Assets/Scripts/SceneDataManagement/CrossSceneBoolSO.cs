using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneBool", menuName = "ScriptableObjects/CrossSceneBool")]
public class CrossSceneBoolSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    bool _value;
    //public bool Value
    //{
    //    get => _value;
    //    set => ((_value != (_value = value)) ? Event : null)?.Invoke();
    //}
    public bool Value
    {
        get => _value;
        set { if (_value != value) /*then*/ { _value = value; /*and*/ Event?.Invoke(); }}
    }


    //false
    //(false) = (false) == (true) => true
    //false
    //(false) = (false) == (false) => false
    //true
    //(true) = (true) == (true) => true
    //true
    //(true) = (true) == (false) => false

    //public bool Value
    //{
    //    get
    //    {
    //        return _value;
    //    }
    //    set
    //    {
    //        if (_value != value)
    //        {
    //            _value = value;
    //            Event?.Invoke();
    //        }
    //    }
    //}
}
