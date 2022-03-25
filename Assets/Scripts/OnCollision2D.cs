using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class CollisionEvent : UnityEvent<Collision2D> { }

[RequireComponent(typeof(Collider2D))]
public class OnCollision2D : MonoBehaviour
{
    [SerializeField] CollisionEvent onCollisionEnter;
    [SerializeField] CollisionEvent onCollisionStay;
    [SerializeField] CollisionEvent onCollisionExit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        onCollisionEnter.Invoke(other);
    }
}
