using UnityEngine;

public class MBDataObject : MonoBehaviour
{
    public virtual void OnValidate()
    {
        MBDataObjectReferences mbdoRefs = GetComponent<MBDataObjectReferences>();
        if (mbdoRefs != null && mbdoRefs.mbDataObjects.Contains(this) != true)
        {
            Debug.LogWarning($"Warning: {this} has not been added to {mbdoRefs}, adding now...");
            mbdoRefs.mbDataObjects.Add(this);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(mbdoRefs);
#endif
        }
    }
    public virtual void Reset()
    {
        MBDataObjectReferences mbdoRefs = GetComponent<MBDataObjectReferences>();
        if (mbdoRefs != null && mbdoRefs.mbDataObjects.Contains(this) != true)
        {
            Debug.LogWarning($"Warning: {this} has not been added to {mbdoRefs}, adding now...");
            mbdoRefs.mbDataObjects.Add(this);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(mbdoRefs);
#endif
        }
    }
}