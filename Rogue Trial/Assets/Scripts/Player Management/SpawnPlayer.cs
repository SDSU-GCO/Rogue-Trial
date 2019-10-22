using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class SpawnPlayer : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneSceneDataSO crossSceneSceneDataSO;
    [SerializeField, Required]
    CrossSceneTransformSO playerTransformSO;
    Transform[] childTransforms;

    [SerializeField, BoxGroup("Component Refs")]
    SpriteRenderer spriteRenderer = null;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    Transform target = null;
    private void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        bool loop=true;
        foreach(Transform t in childTransforms.TakeWhile( t => { return loop; }))
        {
            if (crossSceneSceneDataSO.previousScene.name == t.name)
            {
                loop = false;
                target = t;
            }
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }
    private void Start()
    {
        if (playerTransformSO == null)
            Debug.LogError("crossSceneDataSO null in: "+this);
        playerTransformSO.value.position = target == null ? transform.position : target.position;
    }
}
