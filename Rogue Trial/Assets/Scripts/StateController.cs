using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using NaughtyAttributes;
using OneLine;

public class StateController : MonoBehaviour
{
    [SerializeField, ReorderableList]
    List<MyKVP> RulesTemplate = new List<MyKVP>();

    [System.Serializable]
    public struct MyKVP
    {
        [SerializeField, OneLineWithHeader, HideLabel]
        public Key IfConditions;
        [SerializeField, OneLineWithHeader, HideLabel]
        public Value SetStates; 
    }   [System.Serializable]
    public struct Key
    {
        public enum IfCheck { GameState, Menu, GameState_And_Menu }
        [SerializeField]
        public IfCheck ifCheck;
        [ShowIf("SetIsGameState"), Width(140)]
        public CustomGCOTypes.GameState gameState;
        [ShowIf("SetIsMenu"), Width(40)]
        public bool Menu;
        public bool SetIsGameState() => ifCheck == IfCheck.GameState || ifCheck == IfCheck.GameState_And_Menu;
        public bool SetIsMenu() => ifCheck == IfCheck.Menu || ifCheck == IfCheck.GameState_And_Menu;
        //public bool SetIsGameStateAndMenu() => use == Use.GameState_And_Menu;
    } 

    [System.Serializable]
    public struct Value
    {
        public enum ThenSet { Movement, EnableInput, Movement_And_EnableInput}
        public ThenSet thenSet;
        [HideIf("SetIsEnableInput"), HideLabel, Width(130)]
        public CustomGCOTypes.MovementState movementState;
        [HideIf("SetIsMovement"), Width(90), HideLabel]
        public bool EnableInputs;
        bool SetIsMovement() => thenSet == ThenSet.Movement;
        bool SetIsEnableInput() => thenSet == ThenSet.EnableInput;
        bool SetIsMovementAndEnableInput() => thenSet == ThenSet.Movement_And_EnableInput;
    }

    [SerializeField, ReadOnly]
    List<MonoBehaviour> movables = new List<MonoBehaviour>();
    [SerializeField, ReadOnly]
    List<MonoBehaviour> usesInputs = new List<MonoBehaviour>();

    [SerializeField]
    GameStateSO gameStateSO;
    
    public void OnValidate()
    {
        IMovable[] movables1 = GetComponents<IMovable>();
        IUsesInput[] usesInputs1 = GetComponents<IUsesInput>();
        movables.Clear();
        usesInputs.Clear();
        foreach(IMovable movable in movables1)
        {
            movables.Add((MonoBehaviour)movable);
        }
        foreach (IUsesInput usesInput in usesInputs1)
        {
            usesInputs.Add((MonoBehaviour)usesInput);
        }
        if (gameStateSO == null)
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
    }
    public void ApplyRules()
    {
        foreach (MyKVP kvp in RulesTemplate)
        {
            bool applyThisRule = false;
            switch (kvp.IfConditions.ifCheck)
            {
                case Key.IfCheck.GameState:
                    applyThisRule = kvp.IfConditions.gameState == gameStateSO.gameState;
                    break;
                case Key.IfCheck.Menu:
                    applyThisRule = kvp.IfConditions.Menu == gameStateSO.MenuOpen;
                    break;
                case Key.IfCheck.GameState_And_Menu:
                    applyThisRule = kvp.IfConditions.Menu == gameStateSO.MenuOpen && kvp.IfConditions.gameState == gameStateSO.gameState;
                    break;
            }
            if (applyThisRule)
            {
                switch (kvp.SetStates.thenSet)
                {
                    case Value.ThenSet.Movement:
                        foreach (IMovable movable in movables)
                        {
                            movable.MovementState = kvp.SetStates.movementState;
                        }
                        break;
                    case Value.ThenSet.EnableInput:
                        foreach (IUsesInput usesInput in usesInputs)
                        {
                            usesInput.EnableInputs = kvp.SetStates.EnableInputs;
                        }
                        break;
                    case Value.ThenSet.Movement_And_EnableInput:
                        foreach (IMovable movable in movables)
                        {
                            movable.MovementState = kvp.SetStates.movementState;
                        }
                        foreach (IUsesInput usesInput in usesInputs)
                        {
                            usesInput.EnableInputs = kvp.SetStates.EnableInputs;
                        }
                        break;
                }
            }
        }
    }
    private void OnEnable()
    {
        if (gameStateSO == null)
            Debug.LogError(this);
        gameStateSO.updatedValue.AddListener(ApplyRules);
    }
    private void OnDisable()
    {
        gameStateSO.updatedValue.RemoveListener(ApplyRules);
    }
}


interface IMovable
{
    CustomGCOTypes.MovementState MovementState { get; set; }
}
interface IUsesInput
{
    bool EnableInputs { get; set; }
}