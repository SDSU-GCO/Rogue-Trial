using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneTransformSO", menuName = "ScriptableObjects/CrossSceneTransformSO")]
public class CrossSceneTransformSO : ScriptableObject
{
    public Transform value;
    public QuickEvent previousSceneChanged = new QuickEvent();

    public Transform Value
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
                previousSceneChanged?.Invoke();
            }
        }
    }
}
