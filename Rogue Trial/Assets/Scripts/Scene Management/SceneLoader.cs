using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    [SerializeField, Required]
    GameStateSO gameStateSO;
    [SerializeField, Required]
    SceneTransitionListenerSO sceneTransitionListenerSO;

    public bool AutomaticallyHandleLoadingSymbol = true;
    bool transitioning;
    private void Awake()
    {
        if(sceneTransitionListenerSO!=null)
        {
            if (sceneTransitionListenerSO.changeScenes == null)
                sceneTransitionListenerSO.changeScenes = new SceneTransitionListenerSO.SceneChangeEvent();
            sceneTransitionListenerSO.changeScenes.AddListener(SwitchToScene);
        }
    }
    private void OnDestroy()
    {
        if (sceneTransitionListenerSO != null)
        {
            if (sceneTransitionListenerSO.changeScenes == null)
                sceneTransitionListenerSO.changeScenes = new SceneTransitionListenerSO.SceneChangeEvent();
            sceneTransitionListenerSO.changeScenes.RemoveListener(SwitchToScene);
        }
    }
    public bool IsTransitioning
    {
        get
        {
            return transitioning;
        }
    }
    string sceneToLoad = "";
    AsyncOperation async;
    void SwitchToScene(string sceneToLoad, MonoBehaviour caller)
    {
        if (IsTransitioning)
            Debug.LogError("Can not load a new scene until the previous transition has finished");
        else
        {
            //gameStateSO.gameState = CustomGCOTypes.GameState.Paused;
            if (LoadingScreen != null && AutomaticallyHandleLoadingSymbol)
                LoadingScreen.SetActive(true);


            transitioning = true;
            this.sceneToLoad = sceneToLoad;

            async = SceneManager.UnloadSceneAsync(caller.gameObject.scene);
            StartCoroutine(waitForLoaded());
        }
    }

    IEnumerator waitForLoaded()
    {
        while (!(async.isDone))
        {
            yield return null;
        }

        async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!(async.isDone))
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));

        if (LoadingScreen != null && AutomaticallyHandleLoadingSymbol)
            LoadingScreen.SetActive(false);
        transitioning = false;
    }

}
