using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private int health = 5;
    private float attackDelay = 2.0f;
    private float originalTimeToLive;
    private float timeToLive = 0.5f;
    public float speed = 5;
    [ReorderableList]
    public List<CustomGCOTypes.CollisionLayerKey> targetLayer = new List<CustomGCOTypes.CollisionLayerKey>();

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
