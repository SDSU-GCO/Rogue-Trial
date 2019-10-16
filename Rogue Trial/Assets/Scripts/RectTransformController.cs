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

    public enum LocalScalingFallback
    {
        Screen,
        Parent
    }
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

    private void Reset() => OnValidate();


    public void SetPos(Vector2 center)
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

        Vector2 currentScreenRes;
        currentScreenRes.x = Screen.width;
        currentScreenRes.y = Screen.height;

        //move object
        rectTransform.position = center * currentScreenRes;

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;

        //restore obj size
        rectTransform.sizeDelta = oldSizeDelta;
    }
    public void SetPos(Vector2 botLeftCorner, Vector2 topRightCorner)
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
        Vector2 targetSize = (topRightCorner - botLeftCorner);
        Vector2 center = (targetSize / 2f) + botLeftCorner;
        Vector2 customResolution;
        customResolution.x =canvasScaler.referenceResolution.x;
        customResolution.y = canvasScaler.referenceResolution.y;

        rectTransform.position = center * customResolution;
        rectTransform.sizeDelta = Vector2.zero;

        Vector2 newSize;
        newSize.x = ((customResolution.x * targetSize.x) - (rectTransform.rect.width*anchorSize.x));
        newSize.y = ((customResolution.y * targetSize.y) - (rectTransform.rect.height* anchorSize.y));
        rectTransform.sizeDelta = newSize;

        //Debug.Log("LCS: "+rectTransform.localScale);
        //Debug.Log("LOS: " + rectTransform.lossyScale);
        //Debug.Log("SD: " + rectTransform.sizeDelta);


        //if (anchorSize.x!=0 && anchorSize.y!=0)
        //    rectTransform.sizeDelta = (targetSize)*(anchorSize*((RectTransform) transform.parent).rect.size);
        //else
        //{
        //    Vector2 newSize = (targetSize) * (anchorSize * ((RectTransform)transform.parent).rect.size);
        //    if (anchorSize.x == 0)
        //        newSize.x = targetSize.x * customResolution.x;
        //    if (anchorSize.y == 0)
        //        newSize.y = targetSize.y * customResolution.y;
        //    rectTransform.sizeDelta = newSize;
        //}

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
    }
    public void SetLocalPos(Vector2 botLeftCorner, Vector2 topRightCorner, LocalScalingFallback localScalingFallback = LocalScalingFallback.Parent)
    {
        //cache values
        Vector2 oldPivot = rectTransform.pivot;
        Vector2 anchorSize = (rectTransform.anchorMax - rectTransform.anchorMin);

        //normalize to prefered interaction mode
        rectTransform.pivot = Vector2.one * 0.5f;

        //perform movements
        RectTransform rectTransformParent = (RectTransform)rectTransform.parent;
        Vector2 parentSize = rectTransformParent.rect.size;

        Vector2 targetSize = (topRightCorner - botLeftCorner);
        Vector2 center = (targetSize / 2f) + botLeftCorner;
        //(botLeftCorner * anchorWidth * parentSize - ((anchorWidth * parentSize) / 2));
        Vector2 tmp;

        Vector2 customResolution;
        customResolution.x = canvasScaler.referenceResolution.x;
        customResolution.y = canvasScaler.referenceResolution.y;

        if (localScalingFallback == LocalScalingFallback.Parent)
        {
            Vector2 lpScale;
            lpScale.x = anchorSize.x == 0 ? 1 : anchorSize.x;
            lpScale.y = anchorSize.y == 0 ? 1 : anchorSize.y;

            //Vector2 AnchorMidpoint=(anchorSize / 2f) +rectTransform.anchorMin;
            //Vector2 AnchorOffset = AnchorMidpoint - (Vector2.one * 0.5f);
            //Vector2 anchoredCenter = (center - (Vector2.one * 0.5f))* lpScale + AnchorOffset;

            //rectTransform.localPosition = ((anchoredCenter * parentSize));

            //rectTransform.sizeDelta = parentSize*lpScale*(targetSize-Vector2.one);
            //Vector2 newSize = rectTransform.sizeDelta;
            //newSize.x = anchorSize.x == 0 ? parentSize.x*lpScale.x*targetSize.x : rectTransform.sizeDelta.x;
            //newSize.y = anchorSize.y == 0 ? parentSize.y * lpScale.y*targetSize.y : rectTransform.sizeDelta.y;
            //rectTransform.sizeDelta = newSize;
            //Debug.Log(rectTransform.sizeDelta);

            if (anchorSize.x == 0 && anchorSize.y != 0)
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                tmp.x = center.x * parentSize.x;
                tmp.y = rectTransform.offsetMin.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
                tmp.x = center.x * parentSize.x;
                tmp.y = rectTransform.offsetMax.y;
                rectTransform.offsetMax = tmp;
            }
            else if (anchorSize.x != 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                tmp.x = rectTransform.offsetMin.x;
                tmp.y = center.y * parentSize.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
                tmp.x = rectTransform.offsetMax.x;
                tmp.y = center.y * parentSize.y;
                rectTransform.offsetMax = tmp;
            }
            else if (anchorSize.x == 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = center * parentSize;
                rectTransform.offsetMax = center * parentSize;
            }
            else
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            }
        }
        else
        {
            if (anchorSize.x == 0 && anchorSize.y != 0)
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                tmp.x = center.x * customResolution.x;
                tmp.y = rectTransform.offsetMin.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
                tmp.x = center.x * customResolution.x;
                tmp.y = rectTransform.offsetMax.y;
                rectTransform.offsetMax = tmp;
            }
            else if (anchorSize.x != 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                tmp.x = rectTransform.offsetMin.x;
                tmp.y = center.y * customResolution.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
                tmp.x = rectTransform.offsetMax.x;
                tmp.y = center.y * customResolution.y;
                rectTransform.offsetMax = tmp;
            }
            else if (anchorSize.x == 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = center * customResolution;
                rectTransform.offsetMax = center * customResolution;
            }
            else
            {
                rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
                rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            }
        }

        if ((anchorSize.y == 0 || anchorSize.x == 0) && localScalingFallback == LocalScalingFallback.Screen)
        {
            Vector2 newSizeDelta = rectTransform.sizeDelta;
            if (anchorSize.x == 0)
            {
                newSizeDelta.x = targetSize.x * customResolution.x;
            }
            if (anchorSize.y == 0)
            {
                newSizeDelta.y = targetSize.y * customResolution.y;
            }
            rectTransform.sizeDelta = newSizeDelta;
        }
        else if (localScalingFallback == LocalScalingFallback.Parent)
        {
            Vector2 newSizeDelta = rectTransform.sizeDelta;
            if (anchorSize.x == 0)
            {
                newSizeDelta.x = targetSize.x * parentSize.x;
            }
            if (anchorSize.y == 0)
            {
                newSizeDelta.y = targetSize.y * parentSize.y;
            }
            rectTransform.sizeDelta = newSizeDelta;
        }

        //Debug.Log("Current Resolution: " + canvasScaler.referenceResolution);

        //restore old interactions:
        rectTransform.pivot = oldPivot;

    }
    public void SetLocalPos(float left, float bottom, float right, float top, LocalScalingFallback localScalingFallback = LocalScalingFallback.Parent)
    {
        Vector2 BotLeftCorn;
        Vector2 topRightCorn;

        BotLeftCorn.x = left;
        BotLeftCorn.y = bottom;

        topRightCorn.x = right;
        topRightCorn.y = top;

        SetLocalPos(BotLeftCorn, topRightCorn, localScalingFallback);
    }
    public void SetPos(float left, float bottom, float right, float top)
    {
        Vector2 BotLeftCorn;
        Vector2 topRightCorn;

        BotLeftCorn.x = left;
        BotLeftCorn.y = bottom;

        topRightCorn.x = right;
        topRightCorn.y = top;

        SetPos(BotLeftCorn, topRightCorn);
    }

    public void SetLocalPos(Vector2 center)
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
            tmp.x = center.x * canvasScaler.referenceResolution.x;
            tmp.y = rectTransform.offsetMin.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = center.x * canvasScaler.referenceResolution.x;
            tmp.y = rectTransform.offsetMax.y;
            rectTransform.offsetMax = tmp;
        }
        else if (anchorSize.x != 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = rectTransform.offsetMin.x;
            tmp.y = center.y * canvasScaler.referenceResolution.y;
            rectTransform.offsetMin = tmp;

            rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
            tmp.x = rectTransform.offsetMax.x;
            tmp.y = center.y * canvasScaler.referenceResolution.y;
            rectTransform.offsetMax = tmp;
        }
        else if(anchorSize.x == 0 && anchorSize.y == 0)
        {
            rectTransform.offsetMin = center * canvasScaler.referenceResolution;
            rectTransform.offsetMax = center * canvasScaler.referenceResolution;
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
    }
}
