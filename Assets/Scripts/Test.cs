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

        // ī�޶� ��ġ�� ���󰡴� �� ������ ���� - �� �ؾ� �ڷ� �и�.
        // ī�޶� �ڽ����� �θ� ��鸮�� ��ũ��Ʈ �� ��浵 ��鸲.
    }
}
