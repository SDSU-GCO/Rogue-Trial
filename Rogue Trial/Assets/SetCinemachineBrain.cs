using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCinemachineBrain : MonoBehaviour
{
    [SerializeField] CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;
    private void Awake()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.value = GetComponent<Cinemachine.CinemachineBrain>();
    }
}
