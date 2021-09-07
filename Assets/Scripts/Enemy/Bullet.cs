using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float mLastTime;
    // Start is called before the first frame update
    void Start()
    {
        mLastTime = Time.time;
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        if (time - mLastTime > 2.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
