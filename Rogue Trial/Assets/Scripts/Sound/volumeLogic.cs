using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;
using UnityEngine.UI;
[RequireComponent(typeof(Scrollbar))]
public class volumeLogic : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    Scrollbar scrollBar;
    [SerializeField, Required]
    AudioMixerGroup audioMixerGroup;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (scrollBar == null)
            {
                scrollBar = GetComponent<Scrollbar>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    private void OnEnable()
    {
        scrollBar.onValueChanged.AddListener(ChangeVolume);
    }
    private void OnDisable()
    {
        if(scrollBar!=null)
        {
            scrollBar.onValueChanged.RemoveListener(ChangeVolume);
        }
    }

    private void Start()
    {
        float val;
        audioMixerGroup.audioMixer.GetFloat(audioMixerGroup.name, out val);
        val = Mathf.Pow(10, val / 20);
        scrollBar.value = val;
    }
     
    public void ChangeVolume(float sliderValue)
    {
        audioMixerGroup.audioMixer.SetFloat(audioMixerGroup.name, Mathf.Log10(scrollBar.value) * 20);
    }
}
