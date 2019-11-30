using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new SceneTransitionListenerSO", menuName = "ScriptableObjects/SceneTransitionListenerSO")]
public class SceneTransitionListenerSO : ScriptableObject
{
    public SceneChangeEvent changeScenes = new SceneChangeEvent();

    public class SceneChangeEvent : QuickEvent<string, MonoBehaviour> { }
}
