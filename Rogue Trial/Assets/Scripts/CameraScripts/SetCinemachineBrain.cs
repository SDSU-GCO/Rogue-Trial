using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCinemachineBrain : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField] CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void Awake()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.Value = GetComponent<Cinemachine.CinemachineBrain>();
    }
}
