using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( BoxCollider2D))]
[RequireComponent(typeof( Rigidbody2D))]
public abstract class AbstractNode : MonoBehaviour
{
    /// <summary>
    /// To find neighboring knots around the knot.
    /// </summary>
    /// <param name="collision2D"></param>
    protected abstract void OnCollisionEnter2D(Collision2D other);
}
