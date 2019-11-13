using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
internal struct MBDOInitializationHelper
{
    private GameObject CardinalObj;
    private MBDataObjectReferences mbDatabaseObjectReferences;
    private MonoBehaviour caller;
    private bool isSetup;
    bool isCardinal;

    public MBDOInitializationHelper(MonoBehaviour callerAkaThis)
    {
        isSetup = default;
        CardinalObj = default;
        mbDatabaseObjectReferences = default;
        caller = default;
        isCardinal=default;

        SetupCardinalSubSystem(callerAkaThis);
    }

    public void SetupCardinalSubSystem(MonoBehaviour callerAkaThis)
    {
        isSetup = false;
        CardinalObj = null;
        mbDatabaseObjectReferences = null;
        caller = null;
        if (callerAkaThis.gameObject.scene != new Scene())
        {
            isCardinal = false;
            isSetup = true;
            caller = callerAkaThis;

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cardinal Subsystem");

            foreach (GameObject go in gameObjects)
            {
                if (go.scene == caller.gameObject.scene && go.scene != new Scene())
                {
                    CardinalObj = go;
                }
            }
            //cardinalSubsystem = GameObject.Find("Cardinal Subsystem");


            if (CardinalObj != null)
            {
                mbDatabaseObjectReferences = CardinalObj.GetComponent<MBDataObjectReferences>();
                if (mbDatabaseObjectReferences == null)
                {
                    Debug.Log("mbDatabaseObjectReferences not found in " + CardinalObj);
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
        if(caller!=null && caller.gameObject.scene != new Scene())
        {
            if (isSetup == false)
            {
                Debug.LogWarning("MBDOInitializationHelper: " + mbdo + "is not set up in::: " + caller);
            }
            else if (CardinalObj != null && mbDatabaseObjectReferences != null)
            {
                if (mbdo == null && CardinalObj.scene != new Scene())
                {
                    Debug.Log("SetupMBDO caller: " + caller + " scene: " + caller.gameObject.scene.name);
                    Debug.Log("SetupMBDO system: " + CardinalObj + " scene: " + CardinalObj.scene.name);
                    if (isCardinal == true)
                        mbDatabaseObjectReferences.TryPopulate(out mbdo);
                    else if(CardinalObj.scene == caller.gameObject.scene)
                        mbDatabaseObjectReferences.TryPopulate(out mbdo);
                }
            }
        }
    }
}