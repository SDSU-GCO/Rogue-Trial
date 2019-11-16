using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FlipSpriteOnVelocity : MonoBehaviour
{
    [SerializeField, Required, BoxGroup("Component Refs")]
    new Rigidbody2D rigidbody2D = null;

    [SerializeField, Required, BoxGroup("Component Refs")]
    SpriteRenderer spriteRenderer = null;

    public bool? forceLookRight=null;

    private void OnValidate()
    {
        Transform tmp = transform; 
        while (rigidbody2D == null && tmp != null)
        {
            rigidbody2D = tmp.GetComponent<Rigidbody2D>();
            tmp = tmp.parent;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        tmp = transform; 
        while (spriteRenderer == null && tmp != null)
        {
            spriteRenderer = tmp.GetComponent<SpriteRenderer>();
            tmp = tmp.parent;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public float deadZone = 0.001f;

    private void FlipWithVelocity()
    {
        if (rigidbody2D.velocity.x < -deadZone)
        {
            spriteRenderer.flipX = true;
        }
        else if (rigidbody2D.velocity.x > deadZone)
        {
            spriteRenderer.flipX = false;
        }

        if (forceLookRight != null)
        {
            if (forceLookRight.Value)
                spriteRenderer.flipX = false;
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlipWithVelocity();
    }
}
