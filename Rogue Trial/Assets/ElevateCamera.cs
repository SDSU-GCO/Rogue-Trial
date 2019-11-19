using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevateCamera : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] int inactivePriority = 80;
    [SerializeField] int activePriority = 100;
    [SerializeField] CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;

    public void Activate()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseInOut;
        else
            Debug.LogError("crossSceneCinemachineBrainSO is null in "+this);
        cinemachineVirtualCamera.Priority = activePriority;
    }
    public void Deactivate()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseInOut;
        else
            Debug.LogError("crossSceneCinemachineBrainSO is null in " + this);
        cinemachineVirtualCamera.Priority = inactivePriority;
    }

}
