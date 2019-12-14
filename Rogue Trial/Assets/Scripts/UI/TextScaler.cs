using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextScaler : MonoBehaviour
{
    [SerializeField, HideInInspector]
    TextMeshProUGUI textMeshPro = null;
    [SerializeField, HideInInspector]
    CanvasScaler canvasScaler = null;
    public float fontSize = 0;
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (textMeshPro == null)
            {
                textMeshPro = GetComponent<TextMeshProUGUI>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }

            RectTransform tmp = GetComponent<RectTransform>();
            while (canvasScaler == null && tmp != null)
            {
                canvasScaler = tmp.GetComponent<CanvasScaler>();
                tmp = (RectTransform)tmp.parent;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    private void Awake()
    {
        fontSize = textMeshPro.fontSize;
    }
    private void Update()
    {
        textMeshPro.fontSize = fontSize * (canvasScaler.scaleFactor) * (Screen.width/ canvasScaler.referenceResolution.x);
        Debug.Log((Screen.width / canvasScaler.referenceResolution.x));
        Debug.Log("screen"+Screen.width);
        Debug.Log("ref"+(canvasScaler.referenceResolution.x));
    }

}
