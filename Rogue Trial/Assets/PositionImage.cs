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
    float top;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float botom;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float left;
    [SerializeField, BoxGroup("Edges"), ShowIf("Edges")]
    float right;

    bool Edges() 
    { 
        return controlMode == ControlMode.FourSides; 
    }
    bool Corners()
    {
        return controlMode == ControlMode.TwoCorners; 
    }
    bool UsePoint()
    {
        return controlMode == ControlMode.SinglePoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlMode == ControlMode.SinglePoint)
        {
            if (operateLocal)
                rectTransformController.setLocalPos(myPoint);
            else
                rectTransformController.setPos(myPoint);
        }
        else if (controlMode == ControlMode.TwoCorners)
        {
            if (operateLocal)
                rectTransformController.setLocalPos(botleftCornerPoint, topRightCornerPoint);
            else
                rectTransformController.setPos(botleftCornerPoint, topRightCornerPoint);
        }
        else if (controlMode == ControlMode.FourSides)
        {
            if (operateLocal)
            {

            }
            else
            {

            }
        }

    }
}
