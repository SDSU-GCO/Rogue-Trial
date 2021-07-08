using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : ConditionalComponent
{
    public override bool Result(Collider2D collision)
    {
        return true;
    }
 
}
