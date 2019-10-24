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

    public enum ScalingMethod
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

    public Vector2 GetPos(ScalingMethod localScalingFallback = ScalingMethod.Parent, bool operateLocal = true)
    {
        Vector2 center = rectTransform.position;
        if (operateLocal == true)
        {
            RectTransform parentTransform = (RectTransform)rectTransform.parent;
            Vector2 scaleFactor = (rectTransform.anchorMax - rectTransform.anchorMin);
            Vector2 offset = default;
            offset.x = scaleFactor.x == 0 ? (((scaleFactor.x / 2f) + rectTransform.anchorMin.x) + 0.5f) * parentTransform.rect.size.x : ((scaleFactor.x / 2f) + rectTransform.anchorMin.x) * parentTransform.rect.size.x;
            offset.y = scaleFactor.y == 0 ? (((scaleFactor.y / 2f) + rectTransform.anchorMin.y) + 0.5f) * parentTransform.rect.size.y : ((scaleFactor.y / 2f) + rectTransform.anchorMin.y) * parentTransform.rect.size.y;
            center += offset;
            center -= ((Vector2)parentTransform.position);
            if (localScalingFallback == ScalingMethod.Parent)
            {
                scaleFactor.x = scaleFactor.x == 0 ? 1 : scaleFactor.x;
                scaleFactor.y = scaleFactor.y == 0 ? 1 : scaleFactor.y;
                scaleFactor *= parentTransform.rect.size;
            }
            else
            {
                scaleFactor.x = scaleFactor.x == 0 ? canvasScaler.referenceResolution.x : scaleFactor.x * parentTransform.rect.size.x;
                scaleFactor.y = scaleFactor.y == 0 ? canvasScaler.referenceResolution.y : scaleFactor.y * parentTransform.rect.size.y;
            }
            center = center / (scaleFactor);
        }
        else
        {
            center /= (canvasScaler.referenceResolution);
        }
        return center;
    }
    public Vector2 GetSize(ScalingMethod localScalingFallback = ScalingMethod.Parent, bool operateLocal = true)
    {
        Vector2 size = rectTransform.rect.size;
        if (operateLocal==true)
        {
            RectTransform parentTransform = (RectTransform)rectTransform.parent;
            if (parentTransform == null)
                operateLocal = false;
            else
            {
                Vector2 scaleFactor = (rectTransform.anchorMax - rectTransform.anchorMin);
                if (localScalingFallback == ScalingMethod.Parent)
                {
                    scaleFactor.x = scaleFactor.x == 0 ? 1 : scaleFactor.x;
                    scaleFactor.y = scaleFactor.y == 0 ? 1 : scaleFactor.y;
                }
                else
                {
                    scaleFactor.x = scaleFactor.x == 0 ? canvasScaler.referenceResolution.x : scaleFactor.x;
                    scaleFactor.y = scaleFactor.y == 0 ? canvasScaler.referenceResolution.y : scaleFactor.y;
                }
                size /= (parentTransform.rect.size * scaleFactor);
            }
        }
        else
        {
            size /= (canvasScaler.referenceResolution);
        }

        return size;
    }

    public void SetPos(Vector2 center, bool operateLocal = true)
    { 
        if (operateLocal) 
            SetLocalPos(center, ScalingMethod.Parent); 
        else SetPos(center); 
    }
    public void SetPos(Vector2 center, ScalingMethod localScalingFallback = ScalingMethod.Parent) => SetLocalPos(center, localScalingFallback);
    public void SetPos(Vector2 botLeftCorner, Vector2 botRightCorner, bool operateLocal = true)
    { 
        if (operateLocal) 
            SetLocalPos(botLeftCorner, botRightCorner, ScalingMethod.Parent); 
        else SetPos(botLeftCorner, botRightCorner);
    }
    public void SetPos(Vector2 botLeftCorner, Vector2 botRightCorner, ScalingMethod localScalingFallback = ScalingMethod.Parent) => SetLocalPos(botLeftCorner, botRightCorner, localScalingFallback);
    public void SetPos(float left, float bottom, float right, float top, bool operateLocal = true)
    { 
        if (operateLocal) 
            SetLocalPos(left, bottom, right, top, ScalingMethod.Parent); 
        else SetPos(left, bottom, right, top); 
    }
    public void SetPos(float left, float bottom, float right, float top, ScalingMethod localScalingFallback = ScalingMethod.Parent) => SetLocalPos(left, bottom, right, top, localScalingFallback);
    private void SetPos(Vector2 center)
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
        Vector3 pos = rectTransform.position;
        pos.x = center.x * currentScreenRes.x;
        pos.y = center.y * currentScreenRes.y;

        rectTransform.transform.position = pos;

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;

        //restore obj size
        rectTransform.sizeDelta = oldSizeDelta;
    }
    private void SetPos(Vector2 botLeftCorner, Vector2 topRightCorner)
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
    private void SetLocalPos(Vector2 botLeftCorner, Vector2 topRightCorner, ScalingMethod localScalingFallback = ScalingMethod.Parent)
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

        if (localScalingFallback == ScalingMethod.Parent)
        {
            Vector2 lpScale;
            lpScale.x = anchorSize.x == 0 ? 1 : anchorSize.x;
            lpScale.y = anchorSize.y == 0 ? 1 : anchorSize.y;

            Vector2 AnchorMidpoint = (anchorSize / 2f) + rectTransform.anchorMin;
            Vector2 AnchorOffset = AnchorMidpoint - (Vector2.one * 0.5f);
            Vector2 anchoredCenter = (center - (Vector2.one * 0.5f)) * lpScale + AnchorOffset;

            rectTransform.localPosition = ((anchoredCenter * parentSize));

            rectTransform.sizeDelta = parentSize * lpScale * (targetSize - Vector2.one);
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = anchorSize.x == 0 ? parentSize.x * lpScale.x * targetSize.x : rectTransform.sizeDelta.x;
            newSize.y = anchorSize.y == 0 ? parentSize.y * lpScale.y * targetSize.y : rectTransform.sizeDelta.y;
            rectTransform.sizeDelta = newSize;
            Debug.Log(rectTransform.sizeDelta);

            //if (anchorSize.x == 0 && anchorSize.y != 0)
            //{
            //    rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            //    tmp.x = center.x * parentSize.x;
            //    tmp.y = rectTransform.offsetMin.y;
            //    rectTransform.offsetMin = tmp;

            //    rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            //    tmp.x = center.x * parentSize.x;
            //    tmp.y = rectTransform.offsetMax.y;
            //    rectTransform.offsetMax = tmp;
            //}
            //else if (anchorSize.x != 0 && anchorSize.y == 0)
            //{
            //    rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            //    tmp.x = rectTransform.offsetMin.x;
            //    tmp.y = center.y * parentSize.y;
            //    rectTransform.offsetMin = tmp;

            //    rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            //    tmp.x = rectTransform.offsetMax.x;
            //    tmp.y = center.y * parentSize.y;
            //    rectTransform.offsetMax = tmp;
            //}
            //else if (anchorSize.x == 0 && anchorSize.y == 0)
            //{
            //    rectTransform.offsetMin = center * parentSize;
            //    rectTransform.offsetMax = center * parentSize;
            //}
            //else
            //{
            //    rectTransform.offsetMin = botLeftCorner * anchorSize * parentSize;
            //    rectTransform.offsetMax = (topRightCorner - Vector2.one) * anchorSize * parentSize;
            //}
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

        if ((anchorSize.y == 0 || anchorSize.x == 0) && localScalingFallback == ScalingMethod.Screen)
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
        else if (localScalingFallback == ScalingMethod.Parent)
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
    private void SetLocalPos(float left, float bottom, float right, float top, ScalingMethod localScalingFallback = ScalingMethod.Parent)
    {
        Vector2 BotLeftCorn;
        Vector2 topRightCorn;

        BotLeftCorn.x = left;
        BotLeftCorn.y = bottom;

        topRightCorn.x = right;
        topRightCorn.y = top;

        SetLocalPos(BotLeftCorn, topRightCorn, localScalingFallback);
    }
    private void SetPos(float left, float bottom, float right, float top)
    {
        Vector2 BotLeftCorn;
        Vector2 topRightCorn;

        BotLeftCorn.x = left;
        BotLeftCorn.y = bottom;

        topRightCorn.x = right;
        topRightCorn.y = top;

        SetPos(BotLeftCorn, topRightCorn);
    }

    private void SetLocalPos(Vector2 center, ScalingMethod localScalingFallback = ScalingMethod.Parent)
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
        if(localScalingFallback==ScalingMethod.Parent)
        {

            if (anchorSize.x == 0 && anchorSize.y != 0)
            {
                rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
                tmp.x = center.x * parentSize.x;
                tmp.y = rectTransform.offsetMin.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
                tmp.x = center.x * parentSize.x;
                tmp.y = rectTransform.offsetMax.y;
                rectTransform.offsetMax = tmp;
            }
            else if (anchorSize.x != 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
                tmp.x = rectTransform.offsetMin.x;
                tmp.y = center.y * parentSize.y;
                rectTransform.offsetMin = tmp;

                rectTransform.offsetMax = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
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
                rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
                rectTransform.offsetMax = center * anchorSize * parentSize - ((anchorSize * parentSize) / 2);
            }
        }
        else
        {

            if (anchorSize.x == 0 && anchorSize.y != 0)
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
            else if (anchorSize.x == 0 && anchorSize.y == 0)
            {
                rectTransform.offsetMin = center * canvasScaler.referenceResolution;
                rectTransform.offsetMax = center * canvasScaler.referenceResolution;
            }
            else
            {
                rectTransform.offsetMin = (center * anchorSize * parentSize - ((anchorSize * parentSize) / 2));
                rectTransform.offsetMax = center * anchorSize * parentSize - ((anchorSize * parentSize) / 2);
            }
        }

        //Debug.Log("after adj: " + ((center * anchorWidth * parentSize) - ((anchorWidth * parentSize) / 2)));

        //restore old interaction mode
        rectTransform.anchorMin = oldAnchMin;
        rectTransform.anchorMax = oldAnchMax;
        rectTransform.pivot = oldPivot;
        rectTransform.sizeDelta = oldSizeDelta;
    }
}
