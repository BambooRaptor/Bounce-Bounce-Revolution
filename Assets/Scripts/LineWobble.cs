using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineWobble : MonoBehaviour
{
    // [SerializeField] Transform target;

    [SerializeField] int resolution, waveCount, wobbleCount;
    [SerializeField] float waveSize, animSpeed;


    LineRenderer line;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = resolution;
    }

    // IEnumerator AnimateRope(Vector3 targetPos)
    // {
    //     line.positionCount = resolution;

    //     float percent = 0;
    //     while (percent <= 1f)
    //     {
    //         percent += Time.deltaTime * animSpeed;
    //         SetPoints(targetPos, percent);
    //         yield return null;
    //     }

    //     SetPoints(targetPos, 1);
    // }

    public void AnimateRopeConstant(Vector2 startPos, Vector2 targetPos)
    {
        line.positionCount = resolution;
        line.SetPosition(0, Vector2.zero);

        float lineLength = Vector2.Distance(startPos, targetPos);

        if (lineLength > 2) lineLength = 2;

        line.SetPosition(resolution - 1, new Vector2(0, lineLength));

        StartLines();
        // float percent = 0;
        // while (percent <= 1f)
        // {
        //     percent += Time.deltaTime * animSpeed;
        //     SetPoints(targetPos, percent);
        // }
        // SetPoints(startPos, targetPos, 1);
    }

    void SetPoints(Vector3 startPos, Vector3 targetPos)
    {
        

        // Vector3 ropeEnd = Vector3.Lerp(startPos, targetPos, percent);
        // float length = Vector2.Distance(startPos, ropeEnd);

        // for (int i = 0; i < resolution; i++)
        // {
        //     float xPos = (float)i / resolution * length;
        //     float reversePercent = (1 - percent);

        //     float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI);

        //     float yPos = Mathf.Sin((float)waveCount * i / resolution * 2 * Mathf.PI * reversePercent) * amplitude;

        //     Vector2 pos = new Vector2(yPos, xPos);
        //     line.SetPosition(i, pos);
        // }
    }

    float moveSinWaveCounter = 0;
    private void Update()
    {
        Vector2 startPos = line.GetPosition(0);
        Vector2 endPosition = line.GetPosition(resolution - 1);

        float length = Vector2.Distance(startPos, endPosition);

        for (int i = 1; i < resolution - 1; i++)
        {
            SetLinePosition(i, length, moveSinWaveCounter);
            // float xPos = (float)i / resolution * length;
            // float reversePercent = (1 - percent);

            // float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI);

            // float yPos = Mathf.Sin((float)waveCount * i / resolution * 2 * Mathf.PI * reversePercent) * amplitude;

            // Vector2 pos = new Vector2(yPos, xPos);
            // line.SetPosition(i, pos);
        }

        moveSinWaveCounter += Time.deltaTime * animSpeed;

        // if (moveSinWaveCounter > 2) moveSinWaveCounter = 0;
    }

    void SetLinePosition(int pointIndex, float lineLength, float percent) {
        float pointLengthRatio = (float) pointIndex / resolution;

        float xPos = pointLengthRatio * lineLength;

        float damper = Mathf.Sin(pointLengthRatio * Mathf.PI);

        float amplitude = Mathf.Sin((pointLengthRatio * wobbleCount * Mathf.PI) + percent) * waveSize * damper;

        // float yPos = Mathf.Sin(pointLengthRatio * 2 * Mathf.PI * waveCount * percent) * amplitude;
        
        Vector2 pos = new Vector2(amplitude, xPos);
        line.SetPosition(pointIndex, pos);
    }

    public void ClearLines() {
        line.enabled = false;
    }

    public void StartLines() {
        line.enabled = true;
        AnimateRopeConstant(Vector2.zero, Vector2.zero);
    }
 
}
