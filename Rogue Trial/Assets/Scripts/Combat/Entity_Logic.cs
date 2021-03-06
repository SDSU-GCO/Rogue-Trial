﻿using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ByteSheep.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Entity_Logic : MonoBehaviour
{
    public Event_One_Float hpUpdated = new Event_One_Float();
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneEventSO DamagedEventSO;
    [SerializeField]
    CrossSceneEventSO DiedEventSO;
    [SerializeField]
    QuickEvent DamagedEvent = new QuickEvent();
    [SerializeField]
    QuickEvent DiedEvent = new QuickEvent();
    [SerializeField]
    Health healthComponent;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    //entity parameters
    public bool disableColliderOnDeath = true;

    public int health
    {
        get
        {
            if(healthComponent!=null)
            {
                return healthComponent.CurrentHealth;
            }
            else
            {
                Debug.LogError("Attach a health component to " + this + "you dum dum");
                return 1;
            }
        }
        set
        {

            if (healthComponent != null)
            {
                healthComponent.CurrentHealth = value;
            }
            else
            {
                Debug.LogError("Attach a health component to " + this + "you dum dum");
            }
        }
    }

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            if (healthComponent == null)
            {
                healthComponent = GetComponent<Health>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    public GameObject onDeathReplaceWith;

    //initialize ambiguous parameters
    private void Start() => hpUpdated.Invoke(health);
    private void OnEnable() => hpUpdated.Invoke(health);

    private void Update() => invincibility = Mathf.Min(invincibilityTime, invincibility + Time.deltaTime);

    private float invincibility = 0;

    [MinValue(0f)]
    public float invincibilityTime = 0.5f;

    [MinValue(0f)]
    public float timeToFlashOnHit = 0.5f;

    //take damage function
    public void TakeDamage(int amount)
    {
        if (invincibility >= invincibilityTime)
        {
            if (DamagedEventSO != null)
                DamagedEventSO.Event?.Invoke();

            DamagedEvent?.Invoke();

            health -= amount;

            invincibility = 0;

            hpUpdated.Invoke(health);
            if (health <= 0)
            {
                CommitSuduku();
            }
            else
            {
                if (flashCustomColor)
                    StartCoroutine(ChangeColor());
            }
        }
    }

    

public void CommitSuduku()
    {
        if (DiedEventSO != null)
            DiedEventSO.Event.Invoke();

        DiedEvent.Invoke();

        if (gameObject.layer == 11 && onDeathReplaceWith == null)
        {
            Enemy_Logic tmp = GetComponent<Enemy_Logic>();
            if (tmp != null)
            {
                tmp.enabled = false;
            }

            gameObject.layer = 12;
            Animator tempAnimator = GetComponent<Animator>();
            if (tempAnimator != null)
            {
                tempAnimator.SetBool("isSaved", true);
            }

            if (disableColliderOnDeath)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
        else if (onDeathReplaceWith != null)
        {
            Instantiate(onDeathReplaceWith, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private enum GoToColor
    {
        hurtColor, normalColor
    };

    public float flashSpeed = 20f;

    [SerializeField]
#pragma warning disable CS0414
#pragma warning disable IDE0044 // Add readonly modifier
    private bool flashCustomColor = false;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649

    [SerializeField, ShowIf("flashCustomColor")]
    private FlashingColors flashingColors = new FlashingColors();

    [SerializeField, ShowIf("flashCustomColor")]
    private SpriteRenderer spriteRenderer;

    [System.Serializable]
    private class FlashingColors
    {
        [SerializeField]
#pragma warning disable CS0649
#pragma warning disable IDE0044 // Add readonly modifier
        public Color normalColor = Color.white;
#pragma warning restore CS0649,IDE0044
        [SerializeField]
#pragma warning disable CS0649
#pragma warning disable IDE0044 // Add readonly modifier
        public Color hurtColor = Color.red;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649

    }

    private IEnumerator ChangeColor()
    {
        float flashingTime = timeToFlashOnHit;
        GoToColor goToColor = GoToColor.hurtColor;
        float amount = 0;
        while (flashingTime > 0)
        {
            flashingTime -= Time.deltaTime;
            if (goToColor == GoToColor.hurtColor)
            {
                amount += Time.deltaTime * flashSpeed;
            }
            else
            {
                amount -= Time.deltaTime * flashSpeed;
            }

            if (amount >= 1)
            {
                goToColor = GoToColor.normalColor;
            }
            else if (amount <= 0)
            {
                goToColor = GoToColor.hurtColor;
            }

            spriteRenderer.color = Color.Lerp(flashingColors.normalColor, flashingColors.hurtColor, amount);

            yield return null;
        }
        spriteRenderer.color = flashingColors.normalColor;
    }
}


[System.Serializable]
public class Event_One_Float : QuickEvent<float>
{
}