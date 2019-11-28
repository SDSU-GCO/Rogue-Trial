﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class TraverseLevelDoor : TriggerPrompt
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    string sceneToLoad;
    [SerializeField, Required]
    CrossSceneSceneDataSO crossSceneSceneDataSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if(promptMessage=="")
                promptMessage = "Press 'f' to enter \"" + sceneToLoad + "\"";
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    bool loadStarted = false;

    [SerializeField] CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;
    public void LoadSceneAndUnloadThisOne()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.Value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.Cut;


        if (loadStarted != true)
        {
            loadStarted = true;
            crossSceneSceneDataSO.PreviousScene = gameObject.scene;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }

    public override void Trigger() => LoadSceneAndUnloadThisOne();
}
