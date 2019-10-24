using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using ByteSheep.Events;

public class TriggerPrompt : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, BoxGroup("Events")]
    CrossSceneEventSO[] CrossSceneEventsToFire;
    [SerializeField, BoxGroup("Events")]
    QuickEvent activated;
    [SerializeField, Required, BoxGroup("Prompt Settings")]
    TextMeshProUGUI textMeshProReference = null;
    [SerializeField, BoxGroup("Prompt Settings")]
    protected string promptMessage = "Press 'w' to ";
    [SerializeField, BoxGroup("Prompt Settings")]
    protected KeyCode triggerKey = KeyCode.W;
    
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    bool doPrompt = false;
    public void DispPrompt()
    {
        doPrompt = true;
        if (loadPromptTracker != true)
        {
            StartCoroutine("LoadPrompt");
            loadPromptTracker = true;
        }
    }

    private void Awake()
    {

        if (loadPromptTracker != true)
        {
            StartCoroutine(LoadPrompt());
            loadPromptTracker = true;
        }
    }

    bool loadPromptTracker = false;

    IEnumerator LoadPrompt()
    {
        bool promptUp = false;
        if (textMeshProReference != null)
        {
            textMeshProReference.text = "";
            textMeshProReference.enabled = false;
        }
        else
            Debug.LogError("text shouldn't be null in: " + this);
        while (isActiveAndEnabled)
        {
            if (doPrompt == true)
            {

                if (promptUp != true)
                {
                    if (textMeshProReference != null)
                    {
                        textMeshProReference.enabled = true;
                        textMeshProReference.text = promptMessage;
                    }
                    else
                        Debug.LogError("text shouldn't be null in: " + this);
                    promptUp = true;
                }

                if (Input.GetKeyDown(triggerKey) == true)
                {
                    foreach(CrossSceneEventSO crossSceneEvent in CrossSceneEventsToFire)
                    {
                        crossSceneEvent.Event.Invoke();
                    }
                    activated.Invoke();
                    trigger();
                }

                doPrompt = false;
            }
            else
            {
                if (promptUp == true)
                {
                    if (textMeshProReference != null)
                    {
                        textMeshProReference.text = "";
                        textMeshProReference.enabled = false;
                    }
                    else
                        Debug.LogError("text shouldn't be null in: " + this);
                    promptUp = false;
                }
            }
            yield return new WaitForFixedUpdate();
        }

        loadPromptTracker = false;
    }
    public virtual void trigger()
    {

    }
}

