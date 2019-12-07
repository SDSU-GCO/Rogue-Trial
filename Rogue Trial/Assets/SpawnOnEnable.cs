using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnable : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    GameObject prefab;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    private void OnEnable()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
