using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneSceneDataSO", menuName = "ScriptableObjects/CrossSceneSceneDataSO")]
public class CrossSceneSceneDataSO : ScriptableObject
{
    public Scene activeScene;
    public QuickEvent activeSceneChanged = new QuickEvent();
    public Scene ActiveScene
    {
        get
        {
            return activeScene;
        }
        set
        {
            if (activeScene != value)
            {
                activeScene = value;
                activeSceneChanged?.Invoke();
            }
        }
    }
    public Scene previousScene;
    public QuickEvent previousSceneChanged = new QuickEvent();
    public Scene PreviousScene
    {
        get
        {
            return previousScene;
        }
        set
        {
            if (previousScene != value)
            {
                previousScene = value;
                previousSceneChanged?.Invoke();
            }
        }
    }
}
