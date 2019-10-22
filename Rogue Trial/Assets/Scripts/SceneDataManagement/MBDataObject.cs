using UnityEngine;

public class MBDataObject : MonoBehaviour
{
    private void OnValidate()
    {
        MBDataObjectReferences mbdoRefs = GetComponent<MBDataObjectReferences>();
        if (mbdoRefs != null && mbdoRefs.mbDataObjects.Contains(this) != true)
        {
            Debug.LogWarning($"Warning: {this} has not been added to {mbdoRefs}, adding now...");
            mbdoRefs.mbDataObjects.Add(this);
        }
    }
    

}