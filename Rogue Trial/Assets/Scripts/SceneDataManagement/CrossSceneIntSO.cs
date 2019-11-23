using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneIntSO", menuName = "ScriptableObjects/CrossSceneIntSO")]
public class CrossSceneIntSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    public int value;
    public int Value
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
