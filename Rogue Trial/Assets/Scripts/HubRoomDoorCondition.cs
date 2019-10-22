using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HubRoomDoorCondition : ConditionalComponent
{
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;

    enum RoomClearCheck{ combat, platformer, keysRoom }
    [SerializeField]
    List<RoomClearCheck> roomClearChecks = new List<RoomClearCheck>();
    public override bool Result(Collision2D c) => Result();
    public override bool Result(Collider2D c) => Result();
    bool Result()
    {
        bool rtnVal=true;
        if (roomClearChecks.Contains(RoomClearCheck.combat))
            rtnVal = rtnVal && crossSceneDataSO.combat;
        if (roomClearChecks.Contains(RoomClearCheck.platformer))
            rtnVal = rtnVal && crossSceneDataSO.platformer;
        if (roomClearChecks.Contains(RoomClearCheck.keysRoom))
            rtnVal = rtnVal && crossSceneDataSO.keysRoom;
        return rtnVal;
    }


}
