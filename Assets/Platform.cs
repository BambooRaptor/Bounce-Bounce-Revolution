using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class PlatformEvent : UnityEvent<Vector2, Vector2> { }


public class Platform : MonoBehaviour
{

    LineRenderer lineRenderer;
    BoxCollider2D boxCollider;

    bool isDrawing = false;

    [Header("Platform")]
    [SerializeField] float maxLength = 5f;

    [Header("Transforms")]
    [SerializeField] Transform startPlatformPoint;
    [SerializeField] Transform endPlatformPoint;

    [Header("Events")]
    [SerializeField] UnityEvent onBallCollision;
    [SerializeField] PlatformEvent onDrawStart;
    [SerializeField] PlatformEvent whileDrawing;
    [SerializeField] PlatformEvent onDrawEnd;

    Vector2 startingPosition;
    Vector2 StartingPosition
    {
        get => startingPosition;
        set
        {
            startingPosition = value;
            // startPlatformPoint.position = value;
        }
    }

    Vector2 endingPosition;
    Vector2 EndingPosition
    {
        get => endingPosition;
        set
        {
            endingPosition = value;
            endPlatformPoint.position = value;
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void StopDrawingPlatform()
    {
        isDrawing = false;
        onDrawEnd.Invoke(startingPosition, EndingPosition);
    }

    public void StartDrawingPlatform(Vector2 startPos)
    {
        isDrawing = true;
        boxCollider.enabled = true;
        StartingPosition = startPos;
        // startPlatformPoint.localPosition = Vector3.zero;
        lineRenderer.SetPosition(0, Vector3.zero);
        onDrawStart.Invoke(StartingPosition, EndingPosition);
    }

    public void SetPlatformPosition(Vector2 endPos)
    {
        if (!isDrawing) return;

        float lineLength = (endPos - StartingPosition).magnitude;

        if (lineLength > maxLength) lineLength = maxLength;

        EndingPosition = endPos;

        lineRenderer.SetPosition(1, new Vector3(0, lineLength, 0));

        boxCollider.size = new Vector2(0.1f, lineLength);
        boxCollider.offset = new Vector3(0, lineLength / 2);

        transform.position = startingPosition;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, endPos - StartingPosition);

        whileDrawing.Invoke(StartingPosition, EndingPosition);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("ball"))
        {
            onBallCollision.Invoke();
        }
    }

    public void DestroyPlatform()
    {
        // Have to Set isDrawing to true so SetPlatformPosition can run
        isDrawing = true;
        SetPlatformPosition(StartingPosition);
        isDrawing = false;
        boxCollider.enabled = false;
    }
}
