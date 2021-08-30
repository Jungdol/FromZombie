using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public ParticleSystem ps;
    public PlayerMovement pm;

    bool isOne = false;
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (ps != null)
        {
            ParticleSystem.MainModule main = ps.main;

            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                main.startRotationY = -transform.eulerAngles.y * Mathf.Deg2Rad;
            }
        }
        Play();
    }

    public void Play()
    {
        var emission = ps.emission;
        if (pm.anim.GetBool("isDash") && !isOne)
        {
            emission.rateOverTime = 10;
            isOne = true;
        }
        else if(!pm.anim.GetBool("isDash") && isOne)
        {
            emission.rateOverTime = 0;
            isOne = false;
        }
    }
}
