﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger2DEvents : MonoBehaviour
{

    public QuickEvent OnTriggerEnter2DEvent;
    public QuickEvent OnTriggerExit2DEvent;
    public QuickEvent OnTriggerStay2DEvent;

    private void OnTriggerEnter2D( Collider2D collision ) {
        OnTriggerEnter2DEvent.Invoke();
    }
    private void OnTriggerExit2D( Collider2D collision ) {
        OnTriggerExit2DEvent.Invoke();
    }
    private void OnTriggerStay2D( Collider2D collision ) {
        OnTriggerStay2DEvent.Invoke();
    }
}
