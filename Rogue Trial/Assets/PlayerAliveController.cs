using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerAliveController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Entity_Logic entity_Logic;
    [SerializeField]
    MonoBehaviour[] thingsToDisable;
    [SerializeField, Required]
    CrossSceneEvent Died;
    [SerializeField, Required]
    CrossSceneEvent Revived;
    float oldHP;
    private void OnValidate()
    {
        if (entity_Logic == null)
            entity_Logic = GetComponent<Entity_Logic>();
    }
    private void Awake()
    {
        Died.SomeEvent.AddListener(die);
        Revived.SomeEvent.AddListener(res);
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
