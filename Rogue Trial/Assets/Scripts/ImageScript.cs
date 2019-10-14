using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScript : MonoBehaviour
{
    [SerializeField, HideInInspector]
    RectTransform rectTransform = null;

    private void OnValidate()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    [SerializeField]
    public Vector2 myPoint;

    // Update is called once per frame
    void Update()
    {
        setPos(myPoint);
    }

    void setPos(Vector2 center)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 oldSizeDelta = rectTransform.sizeDelta;
        Vector2 anchorWidth = (rectTransform.anchorMax - rectTransform.anchorMin);

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.one * 0.5f;
        rectTransform.anchorMax = Vector2.one * 0.5f;
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        Vector2 screenSize;
        screenSize.y = Screen.height;
        screenSize.x = Screen.width;

        rectTransform.position = center * screenSize;

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
        rectTransform.sizeDelta = oldSizeDelta;
    }

    public void setLocalPos(Vector2 center)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 oldSizeDelta = rectTransform.sizeDelta;
        Vector2 anchorWidth = (rectTransform.anchorMax - rectTransform.anchorMin);

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        Vector2 parentSize = rectTransform.parent.GetComponent<RectTransform>().rect.size;

        rectTransform.offsetMin = (center * anchorWidth * parentSize - ((anchorWidth * parentSize) / 2));
        rectTransform.offsetMax = center * anchorWidth * parentSize - ((anchorWidth * parentSize) / 2);
        //Debug.Log("after adj: " + ((center * anchorWidth * parentSize) - ((anchorWidth * parentSize) / 2)));

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
        rectTransform.sizeDelta = oldSizeDelta;
    }
}
