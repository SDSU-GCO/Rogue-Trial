using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public abstract class TextPrinter : MonoBehaviour
{
    [SerializeField, HideInInspector]
    TextMeshProUGUI textMeshPro = null;
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
        }
    }
    
    void PrintString(string line)
    {
        StartCoroutine(UpdateText(line));
    }

    bool printing = false;
    bool IsPrinting { get => printing; set => printing = value; }
    IEnumerator UpdateText(string line)
    {
        if(IsPrinting)
        {
            IsPrinting = false;
            yield return null;
            yield return null;
        }
        printing = true;
        StringBuilder stringBuilder = new StringBuilder("");
        while(printing)
        {

            yield return null;
        }
        printing = false;
    }
}
