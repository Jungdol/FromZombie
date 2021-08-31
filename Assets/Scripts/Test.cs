using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CameraManager cameraManager;
    public float smoothSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(cameraManager.desiredPosition.x, cameraManager.desiredPosition.y, 0), Time.deltaTime * smoothSpeed);
        //transform.position += new Vector3(1, 1, 0) * Time.deltaTime;

        // 카메라 위치를 따라가는 데 움직일 수록 - 로 해야 뒤로 밀림.
        // 카메라를 자식으로 두면 흔들리는 스크립트 때 배경도 흔들림.
    }
}
