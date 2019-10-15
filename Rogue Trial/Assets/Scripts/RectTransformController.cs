using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RectTransformController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    RectTransform rectTransform = null;

    [SerializeField, HideInInspector]
    CanvasScaler canvasScaler = null;


    private void OnValidate()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        RectTransform tmp = rectTransform;
        while (canvasScaler == null && tmp != null)
        {
            canvasScaler = tmp.GetComponent<CanvasScaler>();
            tmp = (RectTransform) tmp.parent;
        }
    }

    private void Reset()
    {
        OnValidate();
    }


    public void setPos(Vector2 center)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 oldSizeDelta = rectTransform.sizeDelta;

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.one * 0.5f;
        rectTransform.anchorMax = Vector2.one * 0.5f;
        rectTransform.pivot = Vector2.one * 0.5f;

        //move object
        rectTransform.position = center * canvasScaler.referenceResolution;

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;

        //restore obj size
        rectTransform.sizeDelta = oldSizeDelta;
    }
    public void setPos(Vector2 botLeftCorner, Vector2 topRightCorner)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.one * 0.5f;
        rectTransform.anchorMax = Vector2.one * 0.5f;
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        Vector2 targetSize = topRightCorner - botLeftCorner;
        Vector2 center = (targetSize/2f) + botLeftCorner;
        rectTransform.position = center * canvasScaler.referenceResolution;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        //Vector2 anchorSize = (rectTransform.anchorMax - rectTransform.anchorMin);
        Debug.Log("Current Resolution: " + canvasScaler.referenceResolution);
        rectTransform.sizeDelta = targetSize * canvasScaler.referenceResolution;


        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
    }
    public void setLocalPos(Vector2 botLeftCorner, Vector2 topRightCorner)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 anchorSize = (rectTransform.anchorMax - rectTransform.anchorMin);

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        RectTransform rectTransformParent = rectTransform.parent.GetComponent<RectTransform>();
        Vector2 parentSize = rectTransformParent.rect.size;

        Vector2 targetSize = (topRightCorner - botLeftCorner);
        Vector2 center = (targetSize / 2f) + botLeftCorner;
        //(botLeftCorner * anchorWidth * parentSize - ((anchorWidth * parentSize) / 2));
        Vector2 tmp;

        if (anchorSize.x == 0 && anchorSize.y != 0)
        {
            rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            tmp.x = center.x;
            tmp.y = rectTransform.offsetMin.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            tmp.x = center.x;
            tmp.y = rectTransform.offsetMax.y;
            rectTransform.offsetMax = tmp;
        }
        else if (anchorSize.x != 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            tmp.x = rectTransform.offsetMin.x;
            tmp.y = center.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            tmp.x = rectTransform.offsetMax.x;
            tmp.y = center.y;
            rectTransform.offsetMax = tmp;
        }
        else if (anchorSize.x == 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = center;
            rectTransform.offsetMax = center;
        }
        else
        {
            rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
        }


        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        Debug.Log("Current Resolution: " + canvasScaler.referenceResolution);


        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
    }

    public Vector2 setLocalPos(Vector2 center)
    {
        //cache old values
        Vector2 oldAnchMin = rectTransform.anchorMin;
        Vector2 oldAnchMax = rectTransform.anchorMax;
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 oldSizeDelta = rectTransform.sizeDelta;
        Vector2 anchorSize = (rectTransform.anchorMax - rectTransform.anchorMin);

        //normalize to prefered interaction mode
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        Vector2 parentSize = rectTransform.parent.GetComponent<RectTransform>().rect.size;

        Vector2 tmp;
        if(anchorSize.x==0 && anchorSize.y!=0)
        {
            rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = center.x;
            tmp.y = rectTransform.offsetMin.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = center.x;
            tmp.y = rectTransform.offsetMax.y;
            rectTransform.offsetMax = tmp;
        }
        else if (anchorSize.x != 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = rectTransform.offsetMin.x;
            tmp.y = center.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = rectTransform.offsetMax.x;
            tmp.y = center.y;
            rectTransform.offsetMax = tmp;
        }
        else if(anchorSize.x == 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = center;
            rectTransform.offsetMax = center;
        }
        else
        {
            rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            rectTransform.offsetMax = center * anchorSize * parentSize - ((anchorSize * parentSize) / 2);
        }

        //Debug.Log("after adj: " + ((center * anchorWidth * parentSize) - ((anchorWidth * parentSize) / 2)));

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
        rectTransform.sizeDelta = oldSizeDelta;
        return (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
    }
}
