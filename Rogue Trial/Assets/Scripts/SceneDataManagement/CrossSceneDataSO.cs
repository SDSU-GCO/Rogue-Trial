using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "new CrossSceneDataSO", menuName = "ScriptableObjects/CrossSceneDataSO")]
public class CrossSceneDataSO : ScriptableObject
{
    public bool combat = false;
    public bool platformer = false;
    public bool keysRoom = false;
    public Transform playerTransform = null;
    public Scene activeScene;
}
