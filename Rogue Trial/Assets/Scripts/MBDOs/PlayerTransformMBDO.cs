using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine;

public class PlayerTransformMBDO : MBDataObject
{
    public Transform playerTransform;
    public UnityEvent update;
    public override void OnValidate()
    {
        Debug.Log("OnValidate: " + this + " scene: " + gameObject.scene.name);
        if (playerTransform == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null && go.scene != new UnityEngine.SceneManagement.Scene() && go.scene == gameObject.scene)
            {
                Debug.LogWarning("Assignment to playerTransformMBDO.playerTransform in: " + this + " scene: " + gameObject.scene.name);
                playerTransform = go.GetComponent<Transform>();
            }
        }
        base.OnValidate();
    }
}