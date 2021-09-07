using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public Transform background;

    public float smoothSpeed = 3;
    public Vector2 offset;
    public Vector3 desiredPosition;
    
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    private Vector2 vector2;
    

    private void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        vector2 = transform.position;
        //desiredPosition = new Vector3(
        //    Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
        //    Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
        //    -10);                                                                                                  // Z
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * smoothSpeed);
        //background.position = Vector2.Lerp(transform.position, new Vector2(desiredPosition.x, desiredPosition.y - 1f), Time.fixedDeltaTime * smoothSpeed);

        desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX, limitMaxX),
            Mathf.Clamp(target.position.y + offset.y, limitMinY, limitMaxY),
            -10);

        transform.position = desiredPosition;

    }

    private void OnDrawGizmosSelected()
    {
        Vector3 p1 = new Vector3(limitMinX, limitMaxY, transform.position.z);
        Vector3 p2 = new Vector3(limitMaxX, limitMaxY, transform.position.z);
        Vector3 p3 = new Vector3(limitMaxX, limitMinY, transform.position.z);
        Vector3 p4 = new Vector3(limitMinX, limitMinY, transform.position.z);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
