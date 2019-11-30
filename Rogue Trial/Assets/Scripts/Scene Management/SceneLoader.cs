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
    bool isLoaded, isUnloaded = false;
    public bool IsTransitioning
    {
        get
        {
            return !(isLoaded && isUnloaded);
        }
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += loaded;
        SceneManager.sceneUnloaded += unloaded;
        sceneTransitionListenerSO.changeScenes.AddListener(SwitchToScene);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= loaded;
        SceneManager.sceneUnloaded -= unloaded;
        sceneTransitionListenerSO.changeScenes.RemoveListener(SwitchToScene);
    }
    string sceneToLoad = "";
    void SwitchToScene(string sceneToLoad, MonoBehaviour caller)
    {
        if (!(isLoaded && isUnloaded))
            Debug.LogError("Can not load a new scene until the previous transition has finished");
        else
        {
            //gameStateSO.gameState = CustomGCOTypes.GameState.Paused;
            if (LoadingScreen != null)
                LoadingScreen.SetActive(true);
            isLoaded = isUnloaded = false;
            this.sceneToLoad = sceneToLoad;
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(caller.gameObject.scene);
        }
    }
    void loaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene != gameObject.scene && scene.name==sceneToLoad)
        {
            sceneToLoad = "";
            SceneManager.SetActiveScene(scene);
        }
        isLoaded = true;
        checkAllLoaded();
    }
    void unloaded(Scene scene)
    {
        isUnloaded = true;
        checkAllLoaded();
    }

    void checkAllLoaded()
    {
        //gameStateSO.gameState = CustomGCOTypes.GameState.PlayMode;
        if (LoadingScreen != null && isLoaded && isUnloaded)
            LoadingScreen.SetActive(false);
    }

}
