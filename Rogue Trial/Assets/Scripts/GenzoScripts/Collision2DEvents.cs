using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[RequireComponent(typeof(Collider2D))]
public class Collision2DEvents : MonoBehaviour
{
    public QuickEvent OnCollisionEnter2DEvent;
    public QuickEvent OnCollisionExit2DEvent;
    public QuickEvent OnCollisionStay2DEvent;

    private void OnCollisionEnter2D( Collision2D collision ) {
        OnCollisionEnter2DEvent.Invoke();
    }
    private void OnCollisionExit2D( Collision2D collision ) {
        OnCollisionExit2DEvent.Invoke();
    }
    private void OnCollisionStay2D( Collision2D collision ) {
        OnCollisionStay2DEvent.Invoke();
    }
}
