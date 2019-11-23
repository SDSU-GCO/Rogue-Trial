
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneCinemachineBrainSO", menuName = "ScriptableObjects/CrossSceneCinemachineBrainSO")]
public class CrossSceneCinemachineBrainSO : ScriptableObject
{
    public QuickEvent Event = new QuickEvent();
    public Cinemachine.CinemachineBrain value;
    public Cinemachine.CinemachineBrain Value
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

