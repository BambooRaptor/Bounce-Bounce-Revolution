using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class WarpEvent : UnityEvent<Vector2> {}

public class Warpable : MonoBehaviour
{
    [SerializeField] WarpEvent onWarp;

    public void WarpTo(Vector2 position) {
        transform.position = position;
        onWarp.Invoke(position);
    }

}
