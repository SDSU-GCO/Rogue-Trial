using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPhysPosAfterLoad : MonoBehaviour
{
    [SerializeField, Min(0)]
    float secondsToWaitBeforeLocking = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lockInPlace());
    }
    IEnumerator lockInPlace()
    {
        yield return new WaitForSeconds(secondsToWaitBeforeLocking);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
