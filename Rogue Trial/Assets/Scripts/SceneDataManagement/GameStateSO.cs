using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

public class GameStateSO : ScriptableObject
{
    public CustomGCOTypes.GameState gameState = CustomGCOTypes.GameState.PlayMode;
    public bool MenuOpen = false;
    public OneBoolEvent showPlayer = new OneBoolEvent();
}

public class OneBoolEvent : QuickEvent<bool> { }
