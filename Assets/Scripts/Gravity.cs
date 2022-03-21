using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravity : MonoBehaviour
{
    [SerializeField] bool applyGravity = true;

    [Header("Gravity Forces")]
    [SerializeField] float risingGravity = 1f;
    [SerializeField] float fallingGravity = 1.5f;

    float currentGravity;

    Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        currentGravity = fallingGravity;
    }

    private void Update() {
        if (body.velocity.y < 0) {
            currentGravity = fallingGravity;
        } else {
            currentGravity = risingGravity;
        }

        if (applyGravity)
            body.AddForce(new Vector2(0, -currentGravity), ForceMode2D.Force);
    }

    public void ApplyImpulse(Vector2 impulse) {
        body.velocity = Vector2.zero;
        body.AddForce(impulse, ForceMode2D.Impulse);
    }
}
