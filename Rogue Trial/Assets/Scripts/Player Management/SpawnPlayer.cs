using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class SpawnPlayer : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneSceneDataSO crossSceneSceneDataSO;
    [SerializeField]
    PlayerTransformMBDO playerTransformMBDO;
    [SerializeField,HideInInspector]
    Transform[] childTransforms;

    [SerializeField, HideInInspector]
    SpriteRenderer[] spriteRenderers = null;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    Transform target = null;
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            SpriteRenderer tmp = GetComponent<SpriteRenderer>();
            if (tmp != null)
                spriteRenderers.Append(tmp);

            childTransforms = GetComponentsInChildren<Transform>();

            if (crossSceneSceneDataSO == null)
            {
#if UNITY_EDITOR
                crossSceneSceneDataSO = AssetManagement.FindAssetByType<CrossSceneSceneDataSO>();
#endif
            }

            if (playerTransformMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;

                //IMPORTNANT STEP!!!
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            if (crossSceneSceneDataSO == null)
            {
#if UNITY_EDITOR
                crossSceneSceneDataSO = AssetManagement.FindAssetByType<CrossSceneSceneDataSO>();

                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            if (playerTransformMBDO == null)
            {
                MBDOInitializationHelper mBDOInitializationHelper = default;

                //IMPORTNANT STEP!!!
                mBDOInitializationHelper.SetupCardinalSubSystem(this);
                mBDOInitializationHelper.SetupMBDO(ref playerTransformMBDO);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    private void Awake()
    {
        bool loop=true;
        target = transform;
        foreach(Transform t in childTransforms.TakeWhile( t => { return loop; }))
        {
            if (crossSceneSceneDataSO.PreviousScene.name == t.name)
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
        if (playerTransformMBDO == null)
            Debug.LogError("playerTransformMBDO null in: " + this);
        playerTransformMBDO.playerTransform.position = target == null ? transform.position : target.position;
    }
}
