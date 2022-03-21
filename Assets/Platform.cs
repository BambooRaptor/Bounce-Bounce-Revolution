using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{

    LineRenderer lineRenderer;
    BoxCollider2D boxCollider;

    [SerializeField] Transform startPlatformPoint;
    [SerializeField] Transform endPlatformPoint;

    [SerializeField] UnityEvent onBallCollision;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    public void SetPlatformPositions(Vector2 startPos, Vector2 endPos) {
        float lineLength = (endPos - startPos).magnitude;

        startPlatformPoint.position = startPos;
        endPlatformPoint.position = endPos;

        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, lineLength, 0));

        boxCollider.size = new Vector2(0.1f, lineLength);
        boxCollider.offset = new Vector3(0, lineLength/2);

        transform.position = startPos;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, endPos - startPos);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("ball")) {
            onBallCollision.Invoke();
        }
    }


}
