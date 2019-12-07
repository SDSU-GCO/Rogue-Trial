using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new CrossSceneSceneDataSO", menuName = "ScriptableObjects/CrossSceneSceneDataSO")]
public class CrossSceneSceneDataSO : ScriptableObject
{
    Scene activeScene;
    public QuickEvent activeSceneChanged = new QuickEvent();
    public Scene ActiveScene
    {
        get => activeScene;
        set { if (activeScene != value) /*then*/ { activeScene = value; /*and*/ activeSceneChanged?.Invoke(); } }
    }
    Scene previousScene;
    public QuickEvent previousSceneChanged = new QuickEvent();
    public Scene PreviousScene
    {
        get => previousScene;
        set { if (previousScene != value) /*then*/ { previousScene = value; /*and*/ previousSceneChanged?.Invoke(); } }
    }
}
