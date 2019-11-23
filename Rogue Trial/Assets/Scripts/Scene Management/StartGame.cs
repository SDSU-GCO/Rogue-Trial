using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class StartGame : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneBoolSO[] roomClearData;
    [SerializeField]
    CrossSceneIntSO CurrentPlayerHealthSO;
    //[SerializeField, Required]
    //CrossSceneTransformSO playerTransformSO2;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public string sceneName;
    [SerializeField, HideInInspector]
    public GameStateSO gameStateSO;

    private void OnValidate()
    {
        if(Application.isEditor)
        
        if (gameStateSO == null)
        {
#if UNITY_EDITOR
            gameStateSO = AssetManagement.FindAssetByType<GameStateSO>();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    [SerializeField] int defaultHP;
    private void Start()
    {
        foreach(CrossSceneBoolSO csb in roomClearData)
        {
            csb.Value = false;
        }
        if(CurrentPlayerHealthSO!=null)
        {
            CurrentPlayerHealthSO.value = defaultHP;
        }
        else
        {
            Debug.LogError("CurrentPlayerHealthSO is null in " + this);
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