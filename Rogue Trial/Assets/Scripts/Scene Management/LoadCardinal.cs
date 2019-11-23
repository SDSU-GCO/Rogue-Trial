using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class LoadCardinal : MonoBehaviour
{
    bool loadStarted = false;
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneSceneDataSO crossSceneSceneData;
    [SerializeField] 
    CrossSceneCinemachineBrainSO crossSceneCinemachineBrainSO;
    [SerializeField] 
    string cardinalSceneName = "Cardinal Scene";
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void Awake()
    {
        crossSceneSceneData.ActiveScene = gameObject.scene;
        LoadCardinalScene();        
    }
    private void Start()
    {
        if (crossSceneCinemachineBrainSO != null)
            crossSceneCinemachineBrainSO.Value.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.Cut;
    }
    private void OnEnable()
    {
        LoadCardinalScene();
        SceneManager.activeSceneChanged+= OnSceneChanged;
    }

    bool enforcingScene = false;
    void OnSceneChanged(Scene oldS, Scene newS)
    {
        bool old = enforcingScene;
        enforcingScene = true;
        if(old!=true)
        {
            SceneManager.SetActiveScene(crossSceneSceneData.ActiveScene);
        }
        enforcingScene = false;
    }


    void LoadCardinalScene()
    {
        if(loadStarted!= true && SceneManager.GetSceneByName(cardinalSceneName) == new Scene())
        {
            loadStarted = true;
            SceneManager.LoadScene(cardinalSceneName, LoadSceneMode.Additive);
        }
    }
}
