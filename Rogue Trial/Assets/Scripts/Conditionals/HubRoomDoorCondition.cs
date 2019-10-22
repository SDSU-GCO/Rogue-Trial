using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HubRoomDoorCondition : ConditionalComponent
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneBoolSO[] roomClearData;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    public override bool Result(Collider2D c)
    {
        bool rtnVal=true;

        foreach(CrossSceneBoolSO csb in roomClearData)
        {
            rtnVal = rtnVal && csb.value;
        }

        return rtnVal;
    }


}
