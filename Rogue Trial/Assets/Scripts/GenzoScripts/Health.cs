using ByteSheep.Events;
using UnityEngine;
using System;
using NaughtyAttributes;

public class Health : MonoBehaviour{
#pragma warning disable CS0649
    [SerializeField] private CrossSceneIntSO _CurrentPlayerHealthSO;
#pragma warning restore CS0649

    [SerializeField][ReadOnly] private int _PreviousHealth = 0;
    [SerializeField][ReadOnly] private int _PreviousMaxHealth = 0;

    [Space()]

    public QuickEvent    EmptyEvent    = new QuickEvent();
    public QuickEvent    MaxEvent      = new QuickEvent();
    public IntQuickEvent DecreaseEvent = new IntQuickEvent();
    public IntQuickEvent IncreaseEvent = new IntQuickEvent();
    public IntQuickEvent InflictEvent  = new IntQuickEvent();
    public IntQuickEvent HealEvent     = new IntQuickEvent();

    [SerializeField] private int _MaxHealth = 10;
    public int MaxHealth
    {
        get => _MaxHealth;
        set => SetMaxHealth(value);
    }

    [SerializeField] [ShowIf("showCurrentHealthField")] private int _CurrentHealth = 10;
    bool showCurrentHealthField() => _CurrentPlayerHealthSO==null;
    [ShowNativeProperty] public int CurrentHealth
    {
        get => _CurrentHealth;
        set => SetCurrentHealth(value);
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            if (_CurrentPlayerHealthSO != null)
            {
                _CurrentHealth = _CurrentPlayerHealthSO.Value;
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }

    private void Awake() {
        if(_CurrentPlayerHealthSO!=null)
            _CurrentHealth = _CurrentPlayerHealthSO.Value;
        CurrentHealth = _CurrentHealth;
        MaxHealth = _MaxHealth;
    }
    private void Update() {
        if(_CurrentHealth != _PreviousHealth) SetCurrentHealth(_CurrentHealth);
        if(_MaxHealth != _PreviousMaxHealth) SetMaxHealth(_MaxHealth);
    }

    private void SetCurrentHealth(int value) { 

        //Max and empty events
        if( value >= _MaxHealth ) {
            MaxEvent.Invoke();
            _CurrentHealth = _MaxHealth;
        } else if( value <= 0 ) {
            EmptyEvent.Invoke();
            _CurrentHealth = 0;
        } else {
            _CurrentHealth = value;
        }
        
        int Difference = _CurrentHealth - _PreviousHealth;

        //Increase and decrese events
        if( _CurrentHealth > _PreviousHealth ) HealEvent.Invoke( Mathf.Abs(Difference) );
        if( _CurrentHealth < _PreviousHealth ) InflictEvent.Invoke( Mathf.Abs(Difference) );

        _PreviousHealth = _CurrentHealth;
        if (_CurrentPlayerHealthSO != null)
            _CurrentPlayerHealthSO.Value = _CurrentHealth;

    }
    private void SetMaxHealth(int value) { 

        if(value <= 0) value = 0;

        if(_PreviousMaxHealth > value) DecreaseEvent.Invoke(value);
        if(_PreviousMaxHealth < value) IncreaseEvent.Invoke(value);

        _PreviousMaxHealth = _MaxHealth = value;
    }
    public int ModifyHealth( int Difference ) {
        //Console.WriteLine($"Modified {Difference} points of damage! current health is {CurrentHealth}");
        return CurrentHealth += Difference;
    }

}