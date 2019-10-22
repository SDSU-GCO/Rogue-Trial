using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using NaughtyAttributes;

public class LoadCardinal : MonoBehaviour
{
    bool loadStarted = false;
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneSceneDataSO crossSceneSceneData;
    [SerializeField] 
    string cardinalSceneName = "Cardinal Scene";
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void Awake()
    {
        crossSceneSceneData.activeScene = gameObject.scene;
        LoadCardinalScene();
    }
    private void OnEnable() => LoadCardinalScene();


    void LoadCardinalScene()
    {
        if(loadStarted!= true && SceneManager.GetSceneByName(cardinalSceneName) == new Scene())
        {
            loadStarted = true;
            SceneManager.LoadScene(cardinalSceneName, LoadSceneMode.Additive);
        }
    }
}
