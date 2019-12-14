using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class Restart : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneEventSO playerRevived;
    [SerializeField, Required]
    CrossSceneSceneDataSO crossSceneSceneDataSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public Scene defaultScene;
    public void RestartLevel()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        playerRevived.Event.Invoke();
        if (crossSceneSceneDataSO.activeScene != new Scene())
        {
            SceneManager.LoadScene(crossSceneSceneDataSO.activeScene.name, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(crossSceneSceneDataSO.activeScene.name);
        }
        else
        {
            Debug.LogError("no current level scene detected!");
        }

    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        playerRevived.Event.Invoke();
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}