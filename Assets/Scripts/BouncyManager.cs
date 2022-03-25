using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BouncyManager : MonoBehaviour
{
    InputActionPhase drawingPhase = InputActionPhase.Canceled;

    [SerializeField] bool platformExists = false;

    Vector2 startPoint;
    Vector2 endPoint;

    // [SerializeField] Transform startPlatformPoint;
    // [SerializeField] Transform endPlatformPoint;
    [SerializeField] Platform platformVisual;

    Camera mainCam;

    private void Awake() {
        mainCam = Camera.main;
    }

    public void onMouseEvent(InputAction.CallbackContext context) {
        // Debug.Log("Mouse Pressed");
        // Debug.Log(context.phase);

        drawingPhase = context.phase;
    }


    private void Update() {
        Vector2 currentMousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        switch (drawingPhase)
        {
            case InputActionPhase.Canceled:
                if (platformExists) platformVisual.StopDrawingPlatform();
                platformExists = false;
                break;
            case InputActionPhase.Performed:
                if (!platformExists) {
                    startPoint = currentMousePos;
                    platformVisual.StartDrawingPlatform(startPoint);
                }
                platformExists = true;
                endPoint = currentMousePos;
                platformVisual.SetPlatformPosition(endPoint);
                break;
            default:
                break;
        }
    }


}
