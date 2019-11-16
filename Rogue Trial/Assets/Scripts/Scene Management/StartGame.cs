using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class StartGame : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneBoolSO[] roomClearData;
    //[SerializeField, Required]
    //CrossSceneTransformSO playerTransformSO2;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public string sceneName;
    [SerializeField, HideInInspector]
    public GameStateSO gameStateSO;

    private void OnValidate()
    {
        if (gameStateSO == null)
        {
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    private void Start()
    {
        foreach(CrossSceneBoolSO csb in roomClearData)
        {
            csb.value = false;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && gameStateSO.MenuOpen!=true)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}