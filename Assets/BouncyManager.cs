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
        Debug.Log("Mouse Pressed");
        // Debug.Log(context.phase);

        drawingPhase = context.phase;
    }


    private void Update() {
        Vector2 currentMousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        switch (drawingPhase)
        {
            case InputActionPhase.Canceled:
                platformExists = false;
                // if (platformExists) break;
                // startPlatformPoint.position = currentMousePos;
                // endPlatformPoint.position = currentMousePos;
                break;
            // case InputActionPhase.Started:
            //     Debug.Log(drawingPhase);
            //     platformExists = true;
            //     startPoint = currentMousePos;
            //     break;
            case InputActionPhase.Performed:
                // Debug.Log(drawingPhase);

                if (!platformExists) {
                    startPoint = currentMousePos;
                    // startPlatformPoint.position = currentMousePos;
                    Debug.Log("Starting new platform");
                }
                platformExists = true;
                endPoint = currentMousePos;
                // endPlatformPoint.position = currentMousePos;
                platformVisual.SetPlatformPositions(startPoint, endPoint);
                break;
            default:
                break;
        }
    }


}
