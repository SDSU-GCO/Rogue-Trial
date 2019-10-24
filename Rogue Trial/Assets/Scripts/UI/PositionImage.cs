﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PositionImage : MonoBehaviour
{
    [Required]
    public RectTransformController rectTransformController = null;

    enum ControlMode
    {
        SinglePoint,
        TwoCorners,
        FourSides
    }
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, BoxGroup("Settings")]
    private ControlMode controlMode = ControlMode.SinglePoint;

    [SerializeField, BoxGroup("Settings")]
    private bool operateLocal=false;

    [SerializeField, BoxGroup("Settings"), ShowIf("operateLocal")]
    private RectTransformController.ScalingMethod localScalingFallback = RectTransformController.ScalingMethod.Parent;

    [SerializeField, BoxGroup("Points"), ShowIf("UsePoint")]
    Vector2 myPoint = new Vector2(0.25f, 0.25f);
    [SerializeField, BoxGroup("Points"), ShowIf("Corners")]
    Vector2 botleftCornerPoint = new Vector2(0.25f,0.25f);
    [SerializeField, BoxGroup("Points"), ShowIf("Corners")]
    Vector2 topRightCornerPoint = new Vector2(0.75f, 0.75f);
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float left = 0.25f;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float bottom = 0.25f;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float right = 0.75f;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float top = 0.75f;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value


#pragma warning disable IDE0051 // Remove unused private members
    bool Edges() => controlMode == ControlMode.FourSides;
    bool Corners() => controlMode == ControlMode.TwoCorners;
    bool UsePoint() => controlMode == ControlMode.SinglePoint;
    bool NeedFallback() => controlMode == ControlMode.TwoCorners || controlMode == ControlMode.FourSides;
#pragma warning restore IDE0051 // Remove unused private members

    // Update is called once per frame
    void Update()
    {
        if (controlMode == ControlMode.SinglePoint)
        {
            if(operateLocal)
                rectTransformController.SetPos(myPoint, localScalingFallback);
            else rectTransformController.SetPos(myPoint, false);
        }
        else if (controlMode == ControlMode.TwoCorners)
        {
            if(operateLocal)
                rectTransformController.SetPos(botleftCornerPoint, topRightCornerPoint, localScalingFallback);
            else rectTransformController.SetPos(botleftCornerPoint, topRightCornerPoint, false);
        }
        else if (controlMode == ControlMode.FourSides)
        {
            if (operateLocal)
                rectTransformController.SetPos(left, bottom, right, top, localScalingFallback);
            else rectTransformController.SetPos(left, bottom, right, top, false);
        }
        Vector2 pos = rectTransformController.GetPos(localScalingFallback, operateLocal);
        Vector3 size = rectTransformController.GetSize(localScalingFallback, operateLocal);
        Debug.Log($"pos:({pos.x},{pos.y}) =:= size:({size.x},{size.y})");
        Debug.Log($"abs pos:({rectTransformController.transform.position.x},{rectTransformController.transform.position.y}) =:= size:({size.x},{size.y})");

    }
}
