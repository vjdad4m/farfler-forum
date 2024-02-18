using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool isRotationEnabled = true;
    public float rotationSpeed = 0.2f;
    public float rangeMetersMin = 1.2f;
    public float rangeMetersMax = 100f;
    [Range(0, 360)]
    public float fovHorizontal = 360f;
    [Range(0, 180)]
    public float fovVertical = 60f;
    public int resolutionHorizontal = 1;
    public int resolutionVertical = 10;

    void Start() {}

    void Update()
    {
        Vector3 position = transform.position + transform.TransformDirection(Vector3.up) / 2;

        RaycastHit hit;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < resolutionHorizontal; i++) {
            for (int j = 0; j < resolutionVertical; j++)
            {
                float angleHorizontal = (i / (float)resolutionHorizontal) * fovHorizontal - fovHorizontal / 2;
                float angleVertical = (j / (float)resolutionVertical) * fovVertical - fovVertical / 2;

                Quaternion rotationHorizontal = Quaternion.AngleAxis(angleHorizontal, transform.up);
                Quaternion rotationVertical = Quaternion.AngleAxis(angleVertical, transform.right);
                Vector3 direction = rotationHorizontal * rotationVertical * transform.forward;

                Vector3 newPosition = position + direction * rangeMetersMin;

                if (Physics.Raycast(newPosition, direction, out hit, rangeMetersMax - rangeMetersMin))
                {
                    Debug.DrawRay(newPosition, direction * (hit.distance), Color.red);
                    // points.Add(newPosition);
                    // points.Add(hit.point);
                }
                else
                {
                    Debug.DrawRay(newPosition, direction * (rangeMetersMax - rangeMetersMin), Color.yellow);
                    // points.Add(newPosition);
                    // points.Add(newPosition + direction * (rangeMetersMax - rangeMetersMin));
                }
            }
        }

        if (isRotationEnabled) {
            transform.Rotate(0, rotationSpeed, 0, Space.Self);
        }
    }
}
