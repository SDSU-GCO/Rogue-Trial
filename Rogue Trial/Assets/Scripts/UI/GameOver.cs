using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class GameOver : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Image image;
    [SerializeField, HideInInspector]
    Animator animator;
    [SerializeField, HideInInspector]
    SpriteRenderer spriteRenderer;
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, HideInInspector, Required]
    GameStateSO gameStateSO;
    [SerializeField, Required, BoxGroup("SO refs")]
    CrossSceneEventSO gameoverCrossSceneEvent;
    [SerializeField, Required, BoxGroup("SO refs")]
    CrossSceneEventSO PlayerRevivedSO;
    [SerializeField, Required, BoxGroup("SO refs")]
    CrossSceneSceneDataSO crossSceneSceneDataSO;
    [SerializeField, Required, BoxGroup("MB refs")]
    ScrollCredits scrollCredits = null;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
#pragma warning disable IDE0044 // Add readonly modifier
    [SerializeField]
    private float secondsToFadeIn = 3;
#pragma warning restore IDE0044 // Add readonly modifier

    private void OnValidate()
    {
        if(Application.isEditor)
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        if (image == null)
        {
            image = GetComponent<Image>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        if (gameStateSO == null)
        {
#if UNITY_EDITOR
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    private void OnEnable()
    {
        gameoverCrossSceneEvent.Event.AddListener(OnDeath);
    }
    private void OnDisable()
    {
        gameoverCrossSceneEvent.Event.RemoveListener(OnDeath);
    }

    public void OnDeath()
    {
        image.enabled = true;
        Color temp = image.color;
        temp.a = 0;
        currentTime = 0;
        image.color = temp;
        StartCoroutine(FadeIn());
    }

    private float currentTime = 0;
    // Update is called once per frame
    bool isOpaque = false;
    private void Update()
    {
        image.sprite = spriteRenderer.sprite;
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && isOpaque == true  && gameStateSO.MenuOpen!=true)
        {
            if (crossSceneSceneDataSO.activeScene != new Scene())
            {
                PlayerRevivedSO.Event.Invoke();
                SceneManager.LoadScene(crossSceneSceneDataSO.activeScene.name, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(crossSceneSceneDataSO.activeScene.name);
            }
            else
            {
                Debug.LogError("no current level scene detected!");
            }
        }
    }
    IEnumerator FadeIn()
    {
        Color temp;
        temp.a = 0;
        while (temp.a != 1)
        {
            currentTime = Mathf.Min(secondsToFadeIn, currentTime + Time.unscaledDeltaTime);
            temp = image.color;
            temp.a = Mathf.InverseLerp(0, secondsToFadeIn, currentTime);
            image.color = temp;
            yield return null;
        }
        isOpaque = true;
        animator.enabled = true;
        scrollCredits.enabled = true;
    }
}