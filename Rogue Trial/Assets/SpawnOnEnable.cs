using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnable : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    private void OnEnable()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
