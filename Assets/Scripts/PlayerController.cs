using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Reset Info")]
    [SerializeField] Vector2 resetLocation = new Vector2(0, 2);

    [Header("Jump Info")]
    [SerializeField] float defaultJumpForce = 5f;

    float currentJumpForce;
    float currentFacingDirection = 1;

    Health health;
    TrailRenderer trailRenderer;
    Gravity gravity;
    Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        gravity = GetComponent<Gravity>();
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();

        currentJumpForce = defaultJumpForce;
    }
    
    public void ApplyJump(float direction = 0) {
        gravity.ApplyImpulse((Vector2.up + Vector2.right * direction ) * currentJumpForce);
    }

    public void ToggleDirectionAndJump() {
        if (body.velocity.x > 0)
            ApplyJump(-1);
        else
            ApplyJump(1);
    }

    public void JumpLeft() {
        ApplyJump(-1);
    }

    public void JumpRight() {
        ApplyJump(1);
    }    

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "hazard") {
            // Debug.Log(other.transform.name);
            DamageDealer damageDealer = other.transform.GetComponent<DamageDealer>();
            if (damageDealer) health.TakeDamage(damageDealer.damageAmount);
        }
    }

    public void Reset() {
        transform.position = resetLocation;
        ClearTrail();
        ApplyJump();
    }

    public void ClearTrail() {
        trailRenderer.Clear();
    }
}
