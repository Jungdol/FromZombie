using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject MainCamera;
    public float speed;
    float tempTr;
    float timer;
    float dir;
    float dir2;

    void FixedUpdate()
    {
        if (timer >= 0.1f)
        {
            dir = tempTr - MainCamera.transform.position.x;
            timer = 0;
        }

        else if (timer >= 0)
        {
            tempTr = MainCamera.transform.position.x;
            timer += Time.deltaTime;
        }
        transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);

            //transform.position = Vector3.Lerp(MainCamera.transform.position, Time.deltaTime * smoothSpeed);
    }
}
