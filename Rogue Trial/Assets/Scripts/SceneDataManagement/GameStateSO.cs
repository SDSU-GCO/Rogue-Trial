using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new GameStateSO", menuName = "ScriptableObjects/GameStateSO")]
public class GameStateSO : ScriptableObject
{
    public CustomGCOTypes.GameState gameState = CustomGCOTypes.GameState.PlayMode;
    public QuickEvent gameStateChanged = new QuickEvent();
    public CustomGCOTypes.GameState GameState
    {
        get => gameState;
        set { if (gameState != value) /*then*/ { gameState = value; /*and*/ gameStateChanged?.Invoke(); } }
    }

    bool menuOpen = false;
    public QuickEvent menuOpenChanged = new QuickEvent();
    public bool MenuOpen
    {
        get => menuOpen;
        set { if (menuOpen != value) /*then*/ { menuOpen = value; /*and*/ menuOpenChanged?.Invoke(); } }
    }

    public OneBoolEvent showPlayer = new OneBoolEvent();
}

public class OneBoolEvent : QuickEvent<bool> { }
