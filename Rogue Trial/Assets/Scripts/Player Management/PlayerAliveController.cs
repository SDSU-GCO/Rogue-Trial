﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerAliveController : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, HideInInspector]
    Entity_Logic entity_Logic;
    [SerializeField]
    MonoBehaviour[] thingsToDisable;
    [SerializeField, Required]
    CrossSceneEventSO Died;
    [SerializeField, Required]
    CrossSceneEventSO Revived;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    int oldHP;
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (entity_Logic == null)
            {
                Debug.LogWarning("entity_Logic is not assigned in " + this + " in " + gameObject.scene.name);
                entity_Logic = GetComponent<Entity_Logic>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    private void OnEnable()
    {
        Died.Event.AddListener(die);
        Revived.Event.AddListener(res);
        oldHP = entity_Logic.health;
    }
    private void OnDisable()
    {
        Died.Event.RemoveListener(die);
        Revived.Event.RemoveListener(res);
        oldHP = entity_Logic.health;
    }
    public void die()
    {
        foreach (MonoBehaviour mb in thingsToDisable)
            mb.enabled = false;
    }
    public void res()
    {
        foreach (MonoBehaviour mb in thingsToDisable)
            mb.enabled = true;
        entity_Logic.health = oldHP;
    }
}
