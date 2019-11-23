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
        get
        {
            return gameState;
        }
        set
        {
            if (gameState != value)
            {
                gameState = value;
                gameStateChanged?.Invoke();
            }
        }
    }

    public bool menuOpen = false;
    public QuickEvent menuOpenChanged = new QuickEvent();
    public bool MenuOpen
    {
        get
        {
            return menuOpen;
        }
        set
        {
            if(menuOpen!=value)
            {
                menuOpen = value;
                menuOpenChanged?.Invoke();
            }
        }
    }

    public OneBoolEvent showPlayer = new OneBoolEvent();
}

public class OneBoolEvent : QuickEvent<bool> { }
