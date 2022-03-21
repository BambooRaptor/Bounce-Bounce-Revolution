using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Warper : MonoBehaviour
{
    [SerializeField] Warper warpTo;

    Collider2D col;

    private void Awake() {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Warpable warpable = other.transform.GetComponent<Warpable>();
        if (warpable == null) return;

        Transform warpableTransform = warpable.transform;

        Vector2 relativePosition = new Vector2(warpableTransform.position.x - transform.position.x , warpableTransform.position.y - transform.position.y);
        Vector2 relativePositionNormalized = new Vector2(relativePosition.x / col.bounds.size.x , relativePosition.y / col.bounds.size.y);

        warpTo.Warp(warpable, relativePositionNormalized);        
    }

    public void Warp(Warpable warpable, Vector2 relativePosition) {
        Vector2 warpPosition = (relativePosition * new Vector2(-1, 1) * col.bounds.size) + new Vector2(transform.position.x, transform.position.y);
        warpable.WarpTo(warpPosition);
    }
}
