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
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;
    [SerializeField] 
    string cardinalSceneName = "Cardinal Scene";
    private void Awake()
    {
        loadCardinal();
    }
    private void OnEnable()
    {
        loadCardinal();
    }

    private void Start()
    {
        if (gameObject == null)
            Debug.LogError("I'm null: "+this);
        crossSceneDataSO.activeScene = gameObject.scene;
    }

    void loadCardinal()
    {
        if(loadStarted!= true && SceneManager.GetSceneByName(cardinalSceneName) == new Scene())
        {
            loadStarted = true;
            SceneManager.LoadScene(cardinalSceneName, LoadSceneMode.Additive);
        }
    }
}
