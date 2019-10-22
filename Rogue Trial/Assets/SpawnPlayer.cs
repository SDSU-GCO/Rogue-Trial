using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO = null;

    [SerializeField, BoxGroup("Component Refs")]
    SpriteRenderer spriteRenderer = null;
    private void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }
    private void Start()
    {
        if (crossSceneDataSO == null)
            Debug.LogError("crossSceneDataSO null in: "+this);
        crossSceneDataSO.playerTransform.position = transform.position;
    }
}
