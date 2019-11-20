using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

public class Blink : MonoBehaviour
{

    private List<Renderer> MeshRenderers = new List<Renderer>();
    [SerializeField] private List<GameObject> MeshRendererObjects = new List<GameObject>();

    private void Awake() {
        MeshRendererObjects.ForEach( x => MeshRenderers.Add( x.GetComponent<Renderer>() ));
        //MeshRenderer = GetComponent<MeshRenderer>();
        //MeshRenderers.ForEach( x => Debug.Log( x.name ) );
    }

    public void BlinkTime(float inOffset, float inRepeatDelay, float inIterations) { 
        IEnumerator Coroutine() {
            yield return new WaitForSeconds(inOffset);
            for( int i = 0; i < inIterations; i++ ) {
                MeshRenderers.ForEach(x => x.enabled = false);
                yield return new WaitForSeconds( inRepeatDelay );
                MeshRenderers.ForEach(x => x.enabled = true);
                yield return new WaitForSeconds( inRepeatDelay );
            }
        }
        StartCoroutine( Coroutine() );
    }

    public void DamageBlink() => BlinkTime(0.0f, 0.1f, 5.0f);
    
}
