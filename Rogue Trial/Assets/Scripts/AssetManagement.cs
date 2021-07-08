using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetManagement : MonoBehaviour
{
    public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
    {
        List<T> assets = new List<T>();
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
#endif
        return assets;
    }
    public static List<GameObject> FindAssetsByPrefab()
    {
        List<GameObject> assets = new List<GameObject>();
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:GameObject");
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
#endif
        return assets;
    }

    public static List<T> FindAssetsByComponent<T>() where T : UnityEngine.Object
    {
        List<T> componentList = new List<T>();
#if UNITY_EDITOR
        List<GameObject> gameObjects = FindAssetsByPrefab();
        foreach(GameObject gameObject in gameObjects)
        {
            T[] components = gameObject.GetComponents<T>();
            foreach(T component in components)
            {
                componentList.Add(component);
            }
        }
#endif
        return componentList;
    }

    public static T FindAssetByType<T>() where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        if (guids.Length>0)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                return asset;
            }
        }
#endif
        return null;
    }

    public static T FindAssetByComponent<T>() where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        List<T> component = new List<T>();
        List<GameObject> gameObjects = FindAssetsByType<GameObject>();
        foreach(GameObject gameObject in gameObjects)
        {
            T rtnVal = gameObject.GetComponent<T>();
            if (rtnVal != null)
                return rtnVal;
        }
#endif

        return null;
    }
}
