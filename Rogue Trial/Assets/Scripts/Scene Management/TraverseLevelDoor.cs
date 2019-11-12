using System.Collections;
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
        promptMessage = "Press 'f' to enter \"" + sceneToLoad + "\"";
    }

    bool loadStarted = false;
    public void LoadSceneAndUnloadThisOne()
    {
        if (loadStarted != true)
        {
            loadStarted = true;
            crossSceneSceneDataSO.previousScene = gameObject.scene;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }

    public override void Trigger() => LoadSceneAndUnloadThisOne();
}
