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
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    bool loadStarted = false;
    public void loadSceneAndUnloadThisOne()
    {
        if(loadStarted!=true)
        {
            loadStarted = true;
            SceneManager.LoadSceneAsync(sceneName);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
    bool doPrompt = false;
    public void loadPromptSceneAndUnloadThisOne()
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
            StartCoroutine("LoadPrompt");
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
                    loadSceneAndUnloadThisOne();
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
