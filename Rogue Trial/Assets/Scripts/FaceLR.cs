﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceLR : MonoBehaviour
{
    [SerializeField, HideInInspector]
    new Rigidbody2D rigidbody2D = null;

    [SerializeField, HideInInspector]
    SpriteRenderer spriteRenderer = null;

    private void OnValidate()
    {
        Transform tmp = transform;
        do
        {
            rigidbody2D = tmp.GetComponent<Rigidbody2D>();
            tmp = tmp.parent;
        } while (rigidbody2D == null && tmp != null);
        tmp = transform;
        do
        {
            spriteRenderer = tmp.GetComponent<SpriteRenderer>();
            tmp = tmp.parent;
        } while (spriteRenderer == null && tmp != null);
    }

    public float deadZone = 0.001f;

    private void FlipWithVelocity()
    {
        if (rigidbody2D.velocity.x < -deadZone)
        {
            spriteRenderer.flipY = true;
        }
        else if (rigidbody2D.velocity.x > deadZone)
        {
            spriteRenderer.flipY = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlipWithVelocity();
    }
}
