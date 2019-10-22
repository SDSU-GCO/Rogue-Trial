using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalComponent : MonoBehaviour
{
    public abstract bool Result(Collider2D collision);
}
