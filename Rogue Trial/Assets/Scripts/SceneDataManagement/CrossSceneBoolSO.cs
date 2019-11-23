using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneBool", menuName = "ScriptableObjects/CrossSceneBool")]
public class CrossSceneBoolSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    public bool value;
    public bool Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                this.value = value;
                Event?.Invoke();
            }
        }
    }
}
