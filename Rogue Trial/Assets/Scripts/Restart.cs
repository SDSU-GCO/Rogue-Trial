using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class Restart : MonoBehaviour
{
    [SerializeField, Required]
    CrossSceneEvent playerRevived;
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;
    public Scene defaultScene;
    public void RestartLevel()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        playerRevived.SomeEvent.Invoke();
        if(crossSceneDataSO.activeScene!=new Scene())
        {
            SceneManager.LoadSceneAsync(crossSceneDataSO.activeScene.name, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(crossSceneDataSO.activeScene.name);
        }
        else
        {
            Debug.LogError("no current level scene detected!");
        }

    }
}