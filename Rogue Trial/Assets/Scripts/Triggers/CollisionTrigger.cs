using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ByteSheep.Events;

public class CollisionTrigger : MonoBehaviour
{

    enum TriggerCondition { Enter, Exit, Stay }
    [SerializeField]
    TriggerCondition triggerCondition = TriggerCondition.Enter;
    enum ConditionalMixing { OR, AND}
    [SerializeField]
    ConditionalMixing conditionalMixing = ConditionalMixing.AND;
    public List<ConditionalComponent> conditionalComponents = new List<ConditionalComponent>();

    public class OneCollider2D : QuickEvent<Collider2D> { };

    public QuickEvent triggered;
    public OneCollider2D collided;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (triggerCondition==TriggerCondition.Enter && checkCondition(otherCollider) == true)
            triggered.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(triggerCondition == TriggerCondition.Enter && checkCondition(collision.collider) ==true)
            collided.Invoke(collision.collider);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (triggerCondition == TriggerCondition.Exit && checkCondition(collision.collider) == true)
            collided.Invoke(collision.collider);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (triggerCondition == TriggerCondition.Exit && checkCondition(otherCollider) == true)
            triggered.Invoke();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (triggerCondition == TriggerCondition.Stay && checkCondition(collision.collider) == true)
            collided.Invoke(collision.collider);
    }
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (triggerCondition == TriggerCondition.Stay && checkCondition(otherCollider) == true)
        {
            triggered.Invoke();
        }
    }

    bool checkCondition(Collider2D collision)
    {
        bool rtnVal = default;
        if (conditionalMixing == ConditionalMixing.AND)
        {
            rtnVal = true;
        }
        else if (conditionalMixing == ConditionalMixing.OR)
        {
            rtnVal = false;
        }
        foreach (ConditionalComponent conditionalComponent in conditionalComponents)
        {
            if (conditionalMixing == ConditionalMixing.AND)
            {
                rtnVal = rtnVal && conditionalComponent.Result(collision);
            }
            else if (conditionalMixing == ConditionalMixing.OR)
            {
                rtnVal = rtnVal || conditionalComponent.Result(collision);
            }
        }
        if (conditionalComponents.Count == 0)
            rtnVal = true;
        return rtnVal;
    }

}
