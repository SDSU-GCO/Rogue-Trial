using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteToImageAnimation : MonoBehaviour
{
    [SerializeField][HideInInspector] Image image;
    [SerializeField][HideInInspector] SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (image == null)
            {
                image = GetComponent<Image>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.sprite != image.sprite)
        {
            image.sprite = spriteRenderer.sprite;
        }
    }
}
