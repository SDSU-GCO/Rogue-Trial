using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class StartGame : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneBoolSO[] roomClearData;
    [SerializeField, Required]
    CrossSceneTransformSO playerTransformSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public string sceneName;

    private void Start()
    {
        foreach(CrossSceneBoolSO csb in roomClearData)
        {
            csb.value = false;
        }
        playerTransformSO.value.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            playerTransformSO.value.gameObject.SetActive(true);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}