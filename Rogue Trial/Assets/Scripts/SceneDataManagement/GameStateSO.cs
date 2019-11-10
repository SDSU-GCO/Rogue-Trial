using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new GameStateSO", menuName = "ScriptableObjects/GameStateSO")]
public class GameStateSO : ScriptableObject
{
    public CustomGCOTypes.GameState gameState = CustomGCOTypes.GameState.PlayMode;
    public bool MenuOpen = false;
    public QuickEvent updatedValue = new QuickEvent();
    public OneBoolEvent showPlayer = new OneBoolEvent();
}

public class OneBoolEvent : QuickEvent<bool> { }
