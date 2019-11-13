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
    PlayerTransformMBDO playerTransformMBDO;
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

        if (crossSceneSceneDataSO == null)
        {
            crossSceneSceneDataSO = AssetManagement.FindAssetByType<CrossSceneSceneDataSO>();
        }

        if (playerTransformMBDO == null)
        {
            Debug.LogWarning("playerTransformMBDO is null in "+this + " scene: "+gameObject.scene.name);
            MBDOInitializationHelper mBDOInitializationHelper = default;

            //IMPORTNANT STEP!!!
            mBDOInitializationHelper.SetupCardinalSubSystem(this);
            mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
        }
    }
    private void Reset()
    {
        if(crossSceneSceneDataSO==null)
        {
            crossSceneSceneDataSO = AssetManagement.FindAssetByType<CrossSceneSceneDataSO>();
        }
        if (playerTransformMBDO == null)
        {
            MBDOInitializationHelper mBDOInitializationHelper = default;

            //IMPORTNANT STEP!!!
            mBDOInitializationHelper.SetupCardinalSubSystem(this);
            mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
        }
    }
    private void Awake()
    {
        bool loop=true;
        target = transform;
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
        Debug.Log("Start: " + this + " scene: " + gameObject.scene.name);
        if (playerTransformMBDO == null)
            Debug.LogError("playerTransformMBDO null in: " + this);
        playerTransformMBDO.playerTransform.position = target == null ? transform.position : target.position;
    }
}
