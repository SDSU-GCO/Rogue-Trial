using UnityEngine;

internal struct MBDOInitializationHelper
{
    private GameObject cardinalSubsystem;
    private MBDataObjectReferences mbDatabaseObjectReferences;
    private MonoBehaviour caller;
    private bool isSetup;

    public MBDOInitializationHelper(MonoBehaviour callerAkaThis)
    {
        isSetup = default;
        cardinalSubsystem = default;
        mbDatabaseObjectReferences = default;
        caller = default;

        Setup(callerAkaThis);
    }

    public void Setup(MonoBehaviour callerAkaThis)
    {
        isSetup = false;
        cardinalSubsystem = null;
        mbDatabaseObjectReferences = null;
        caller = null;
        if (callerAkaThis.gameObject.scene != new UnityEngine.SceneManagement.Scene())
        {
            isSetup = true;
            caller = callerAkaThis;

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cardinal Subsystem");

            foreach (GameObject go in gameObjects)
            {
                if (go.scene == caller.gameObject.scene && go.scene != new UnityEngine.SceneManagement.Scene())
                {
                    cardinalSubsystem = go;
                    Debug.Log("GOs: " + go.scene.name);
                }
            }
            //cardinalSubsystem = GameObject.Find("Cardinal Subsystem");


            if (cardinalSubsystem != null)
            {
                mbDatabaseObjectReferences = cardinalSubsystem.GetComponent<MBDataObjectReferences>();
                if (mbDatabaseObjectReferences == null)
                {
                    Debug.Log("mbDatabaseObjectReferences not found in " + cardinalSubsystem);
                }
            }
            else
            {
                Debug.Log("Cardinal Subsystem not found in " + caller + "'s scene: " + caller.gameObject.scene.name);
            }
        }
    }

    public void SetupMBDO<T>(ref T mbdo) where T : MBDataObject
    {
        if(caller!=null && caller.gameObject.scene != new UnityEngine.SceneManagement.Scene())
        {
            if (isSetup == false)
            {
                Debug.LogWarning("MBDOInitializationHelper: " + mbdo + "is not set up in::: " + caller);
            }
            else if (cardinalSubsystem != null && mbDatabaseObjectReferences != null)
            {
                if (mbdo == null && cardinalSubsystem.scene == caller.gameObject.scene && cardinalSubsystem.scene != new UnityEngine.SceneManagement.Scene())
                {
                    mbDatabaseObjectReferences.TryPopulate(out mbdo);
                }
            }
        }
    }
}