using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class StartGame : MonoBehaviour
{
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;
    public string sceneName;

    private void Start()
    {
        crossSceneDataSO.combat = false;
        crossSceneDataSO.platformer = false;
        crossSceneDataSO.keysRoom = false;
        crossSceneDataSO.playerTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            crossSceneDataSO.playerTransform.gameObject.SetActive(true);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}