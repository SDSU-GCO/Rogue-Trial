using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextScaler : MonoBehaviour
{
    [HideInInspector]
    TextMeshProUGUI textMeshPro = null;
    [SerializeField, HideInInspector]
    CanvasScaler canvasScaler = null;
    public float fontSize = 0;
    private void Reset()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if(textMeshPro==null)
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
    private void Awake()
    {
        OnValidate();
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
