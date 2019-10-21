using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class StartGame : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    CrossSceneDataSO crossSceneDataSO;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public string sceneName;

    private void Start()
    {
        crossSceneDataSO.combat = false;
        crossSceneDataSO.keysRoom = false;
        crossSceneDataSO.platformer = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}