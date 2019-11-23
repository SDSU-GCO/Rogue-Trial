using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

public class PointlessScriptTester : MonoBehaviour
{
    [SerializeField]
    public QuickEvent Event = new QuickEvent();
    [SerializeField]
    bool _value=false;
    public bool Value
    {
        get => _value;
        set => ((_value != (_value=value)) ? Event : null)?.Invoke();
    }

    [SerializeField]
    bool toAssign;
    private void Update()
    {
        Value = toAssign;
    }

    public void Print()
    {
        Debug.Log(_value);
    }
}
