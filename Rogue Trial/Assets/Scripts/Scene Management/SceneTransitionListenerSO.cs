using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new SceneTransitionListenerSO", menuName = "ScriptableObjects/SceneTransitionListenerSO")]
public class SceneTransitionListenerSO : ScriptableObject
{
    public SceneChangeEvent changeScenes;
    private void Awake()
    {
        if(changeScenes==null)
            changeScenes = new SceneChangeEvent();
    }

    private void OnValidate()
    {
        if (changeScenes == null)
        {
            changeScenes = new SceneChangeEvent();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public class SceneChangeEvent : UnityEngine.Events.UnityEvent<string, MonoBehaviour> { }
}
