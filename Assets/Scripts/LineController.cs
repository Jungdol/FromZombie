using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Texture[] textures;

    private int animaionStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f / fps)
        {
            animaionStep++;
            if (animaionStep == textures.Length)
                animaionStep = 0;

            lineRenderer.material.SetTexture("_MainTex", textures[animaionStep]);

            fpsCounter = 0f;
        }
    }
}
