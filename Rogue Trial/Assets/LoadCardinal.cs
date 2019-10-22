using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadCardinal : MonoBehaviour
{
    bool loadStarted = false;
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

    void loadCardinal()
    {
        if(loadStarted!= true && SceneManager.GetSceneByName(cardinalSceneName) == new Scene())
        {
            loadStarted = true;
            SceneManager.LoadScene(cardinalSceneName, LoadSceneMode.Additive);

        }
    }
}
