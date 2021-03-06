﻿using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FollowPath : MonoBehaviour, IMovable
{
    public CustomGCOTypes.MovementState movementState = CustomGCOTypes.MovementState.Enabled;

    public float interpolationRate = 0.5f;
    public AnimationCurve animationCurve = new AnimationCurve();
    [ReorderableList]
    public List<Transform> path;

#pragma warning disable CS0109
    [SerializeField,HideInInspector]
    private new Rigidbody2D rigidbody2D;
#pragma warning restore CS0109
    private float pathProgress;
#pragma warning disable IDE0044 // Add readonly modifier
    private Transform nextTarget;
#pragma warning restore IDE0044 // Add readonly modifier
    private int pathIndex = 0;
    private int nextPathIndex = 1;

    [SerializeField, HideInInspector]
    private SpriteRenderer spriteRenederer;

    public CustomGCOTypes.MovementState MovementState 
    { 
        get => movementState; 
        set => movementState=value; 
    }

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (spriteRenederer == null)
            {
                spriteRenederer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            if (rigidbody2D == null)
            {
                rigidbody2D = GetComponent<Rigidbody2D>();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    private void OnEnable()
    {
        pathIndex = 0;
        nextPathIndex = 1;
        if(path.Count < 2)
        {
            Debug.LogWarning("Warning: path must contain at least two points!  Disabling Enemy_Sideways!");
            enabled = false;
        }

    }
    private void Update()
    {
        //move
        pathProgress += Time.deltaTime * interpolationRate;
        while (pathProgress >= 1)
        {
            pathProgress -= 1;
            AutoIncrement(ref pathIndex);
            AutoIncrement(ref nextPathIndex);
        }
        if (path[pathIndex].position.x < path[nextPathIndex].position.x)
        {
            spriteRenederer.flipX = false;
        }

        if (path[pathIndex].position.x > path[nextPathIndex].position.x)
        {
            spriteRenederer.flipX = true;
        }

        Vector2 temp = Vector2.Lerp(path[pathIndex].position, path[nextPathIndex].position, animationCurve.Evaluate(pathProgress));

        rigidbody2D.MovePosition(temp);

        //if (switchDirection == true && idle >= (moveDelay*2))
        //{
        //    moveVelocity = -moveVelocity;
        //    idle = 0;
        //}
        //else
        //{
        //    rigidbody2D.velocity += Vector2.left*0;
        //}

        //if (timePassed >= moveDelay && rigidbody2D.velocity.x <= 0.0000001)
        //{
        //    rigidbody2D.velocity += Vector2.left* moveVelocity;
        //    timePassed = 0;

        //    switchDirection = true;

        //}
        //else
        //{
        //    switchDirection = false;
        //}

        //timePassed += Time.deltaTime;
        //idle += Time.deltaTime;
    }

    private void AutoIncrement(ref int pathIndex)
    {
        pathIndex++;
        if (pathIndex >= path.Count)
        {
            pathIndex = 0;
        }
    }
}