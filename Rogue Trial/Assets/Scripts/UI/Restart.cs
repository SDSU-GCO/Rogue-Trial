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
    [SerializeField, HideInInspector, Required]
    GameStateSO gameStateSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public Scene defaultScene;
    private void OnValidate()
    {
        if (gameStateSO == null)
        {
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
        }
    }
    public void RestartLevel()
    {
        if (gameStateSO.gameState != CustomGCOTypes.GameState.PlayMode)
        {
            gameStateSO.gameState = CustomGCOTypes.GameState.PlayMode;
            gameStateSO.updatedValue.Invoke();
        }
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
        if (gameStateSO.gameState != CustomGCOTypes.GameState.PlayMode)
        {
            gameStateSO.gameState = CustomGCOTypes.GameState.PlayMode;
            gameStateSO.updatedValue.Invoke();
        }
        playerRevived.Event.Invoke();
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}