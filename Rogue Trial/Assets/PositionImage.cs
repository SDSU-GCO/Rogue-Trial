using System.Collections;
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
    private ControlMode controlMode;

    [SerializeField, BoxGroup("Settings")]
    private bool operateLocal;

    [SerializeField, BoxGroup("Points"), ShowIf("UsePoint")]
    Vector2 myPoint;
    [SerializeField, BoxGroup("Points"), ShowIf("Corners")]
    Vector2 botleftCornerPoint;
    [SerializeField, BoxGroup("Points"), ShowIf("Corners")]
    Vector2 topRightCornerPoint;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float left;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float bottom;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float right;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float top;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value


#pragma warning disable IDE0051 // Remove unused private members
    bool Edges() => controlMode == ControlMode.FourSides;
    bool Corners() => controlMode == ControlMode.TwoCorners;
    bool UsePoint() => controlMode == ControlMode.SinglePoint;
#pragma warning restore IDE0051 // Remove unused private members

    // Update is called once per frame
    void Update()
    {
        if (controlMode == ControlMode.SinglePoint)
        {
            if (operateLocal)
                rectTransformController.SetLocalPos(myPoint);
            else
                rectTransformController.SetPos(myPoint);
        }
        else if (controlMode == ControlMode.TwoCorners)
        {
            if (operateLocal)
                rectTransformController.SetLocalPos(botleftCornerPoint, topRightCornerPoint);
            else
                rectTransformController.SetPos(botleftCornerPoint, topRightCornerPoint);
        }
        else if (controlMode == ControlMode.FourSides)
        {
            if (operateLocal)
            {
                rectTransformController.SetLocalPos(left, bottom, right, top);
            }
            else
            {
                rectTransformController.SetPos(left, bottom, right, top);
            }
        }

    }
}
