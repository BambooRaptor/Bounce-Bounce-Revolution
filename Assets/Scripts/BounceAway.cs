using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAway : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] float minForce;
    [SerializeField] float maxForce;

    // [Header("Direction")]
    // [SerializeField] bool randomizeDirection;
    // [SerializeField] [Range(0, 1)] float directionIntensity;

    public void OnHit(Collision2D body) {
        // // Calculate Angle Between the collision point and the player
        //  Vector3 dir = c.contacts[0].point - transform.position;
        //  // We then get the opposite (-Vector3) and normalize it
        //  dir = -dir.normalized;
        //  // And finally we add force in the direction of dir and multiply it by force. 
        //  // This will push back the player
        //  GetComponent<Rigidbody>().AddForce(dir*force);

        Debug.Log("OnHit Called with " + body.transform.name);

        if (body.transform.CompareTag("ball")) {
            float force = Random.Range(minForce, maxForce);
            Vector2 currentPos = body.transform.position;
            Vector2 dir = body.GetContact(0).point - currentPos;
            Debug.Log("direction of contact: " + dir);
            dir = -dir.normalized;

            Debug.Log("applying force of magiture: " + force);
            body.transform.GetComponent<Gravity>().ApplyImpulse(dir*force);
        }
    }
}
