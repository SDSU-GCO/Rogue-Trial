using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevateCamera : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField] Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] int inactivePriority = 80;
    [SerializeField] int activePriority = 100;
    [SerializeField] CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
     
    public void Activate()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.Value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseInOut;
        else
            Debug.LogError("crossSceneCinemachineBrainSO is null in "+this);
        cinemachineVirtualCamera.Priority = activePriority;
    }
    public void Deactivate()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.Value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseInOut;
        else
            Debug.LogError("crossSceneCinemachineBrainSO is null in " + this);
        cinemachineVirtualCamera.Priority = inactivePriority;
    }

}
