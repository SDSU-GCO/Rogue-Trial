using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void quit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
 #else
         Application.Quit();
 #endif
    }
    public void Quit() => quit();
}