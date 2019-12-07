using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicListener : MonoBehaviour
{
    [SerializeField]
    MusicMessageEventSO musicMessageEventSO = null;
    MusicPlayer musicPlayer;

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (musicPlayer == null)
            {

#if UNITY_EDITOR
                musicPlayer = GetComponent<MusicPlayer>();
#endif
            }

            if (musicMessageEventSO == null)
            {
#if UNITY_EDITOR
                musicMessageEventSO = AssetManagement.FindAssetByType<MusicMessageEventSO>();
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    private void OnEnable()
    {
        if (musicMessageEventSO != null)
            musicMessageEventSO.Event.AddListener(SetTracks);
    }
    private void OnDisable()
    {
        if (musicMessageEventSO != null)
            musicMessageEventSO.Event.RemoveListener(SetTracks);
    }
    void SetTracks()
    {
        if (musicPlayer != null && musicMessageEventSO != null)
            musicPlayer.tracks = musicMessageEventSO.AudioClips;
    }
}
