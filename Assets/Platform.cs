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

    [Header("Events")]
    [SerializeField] UnityEvent onDrawStart;
    [SerializeField] UnityEvent whileDrawing;
    [SerializeField] UnityEvent onDrawEnd;

    Vector2 startingPosition;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void StartDrawingPlatform(Vector2 startPos) {
        startingPosition = startPos;
        startPlatformPoint.localPosition = Vector3.zero;
        lineRenderer.SetPosition(0, Vector3.zero);
    }

    public void SetPlatformPositions(Vector2 endPos) {
        float lineLength = (endPos - startingPosition).magnitude;

        // startPlatformPoint.position = startPos;
        endPlatformPoint.position = endPos;

        // lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, lineLength, 0));

        boxCollider.size = new Vector2(0.1f, lineLength);
        boxCollider.offset = new Vector3(0, lineLength/2);

        transform.position = startingPosition;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, endPos - startingPosition);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("ball")) {
            onBallCollision.Invoke();
        }
    }
}
