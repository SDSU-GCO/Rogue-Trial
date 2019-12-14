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

    SpriteRenderer[] spriteRenderers = null;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    Transform target = null;
    private void OnValidate()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer tmp = GetComponent<SpriteRenderer>();
        if(tmp!=null)
            spriteRenderers.Append(tmp);

        childTransforms = GetComponentsInChildren<Transform>();
    }
    private void Awake()
    {
        bool loop=true;
        foreach(Transform t in childTransforms.TakeWhile( t => { return loop; }))
        {
            if (crossSceneSceneDataSO.previousScene.name == t.name)
            {
                loop = false;
                target = t;
            }
        }

        foreach(SpriteRenderer sr in spriteRenderers)
            sr.enabled = false;
    }
    private void Start()
    {
        if (playerTransformSO == null)
            Debug.LogError("crossSceneDataSO null in: "+this);
        playerTransformSO.value.position = target == null ? transform.position : target.position;
    }
}
