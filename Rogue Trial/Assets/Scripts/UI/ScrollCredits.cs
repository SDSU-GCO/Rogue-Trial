using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScrollCredits : MonoBehaviour
{
    [SerializeField, HideInInspector]
    RectTransform rectTransform = null;
    [SerializeField, HideInInspector]
    TextMeshProUGUI textMeshProUGUI = null;
    [SerializeField, HideInInspector]
    Canvas canvas = null;
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Min(0)]
    float scrollSpeed;
    [SerializeField, Required]
    TextAsset textAsset;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void OnValidate()
    {
        if (textMeshProUGUI == null)
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = textAsset.text;
        }
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if (canvas==null)
        {
            RectTransform tmp = rectTransform;
            while (canvas == null && tmp != null)
            {
                canvas = tmp.GetComponent<Canvas>();
                tmp = (RectTransform)tmp.parent;
            }
        }
    }

    bool notStarted=true;
    // Update is called once per frame
    void Update()
    {
        if(notStarted==true)
            StartCoroutine(Scroll());
    }
    IEnumerator Scroll()
    {
        notStarted = false;
        while(true)
        {
            transform.position += (Vector3.up * scrollSpeed * canvas.scaleFactor * Time.unscaledDeltaTime);
            if (transform.position.y >= rectTransform.rect.height + canvas.scaleFactor * ((RectTransform)canvas.transform).rect.height * 1.05)
            {
                Vector3 Pos = transform.position;
                Pos.y = 0;
                transform.position = Pos;
            }
            yield return null;
        }
    }
}
