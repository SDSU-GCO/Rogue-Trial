using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    string sceneName;
    [SerializeField]
    string promptMessage = "proceed to... ";
    [SerializeField]
    TextMeshProUGUI text = null;
    [SerializeField, Required]
    CrossSceneEvent crossSceneEvent;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    bool loadStarted = false;
    public void LoadSceneAndUnloadThisOne()
    {
        if(loadStarted!=true)
        {
            loadStarted = true;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            crossSceneEvent.SomeEvent.Invoke();
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
    bool doPrompt = false;
    public void LoadPromptSceneAndUnloadThisOne()
    {
        doPrompt = true;
        if(loadPromptTracker!=true)
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
        if (text != null)
        {
            text.text = "";
            text.enabled = false;
        }
        else
            Debug.LogError("text shouldn't be null in: " + this);
        while (isActiveAndEnabled)
        {
            if (doPrompt==true)
            {

                if (promptUp!=true)
                {
                    if (text != null)
                    {
                        text.enabled = true;
                        text.text = promptMessage;
                    }
                    else
                        Debug.LogError("text shouldn't be null in: "+this);
                    promptUp = true;
                }

                if(Input.GetKey(KeyCode.W)==true)
                {
                    LoadSceneAndUnloadThisOne();
                }

                doPrompt = false;
            }
            else
            {
                if(promptUp==true)
                {
                    if (text != null)
                    {
                        text.text = "";
                        text.enabled = false;
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
}
