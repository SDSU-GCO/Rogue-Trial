using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "new CrossSceneSceneDataSO", menuName = "ScriptableObjects/CrossSceneSceneDataSO")]
public class CrossSceneSceneDataSO : ScriptableObject
{
    public Scene activeScene;
    public Scene previousScene;
}
