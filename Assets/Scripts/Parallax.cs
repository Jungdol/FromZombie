using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Tooltip("메인 카메라 안에 있는 카메라 매니저")]
    public CameraManager cameraManager;
    public float speed;
    [SerializeField]
    public ParallaxElement[] Elements;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ParallaxElement e in Elements)
        {
            e.Start();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(cameraManager.transform.position.x, cameraManager.transform.position.y + 1f);
        //transform.position += new Vector3(1, 1, 0) * Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, cameraManager.transform.position, speed * Time.deltaTime);


        foreach (ParallaxElement e in Elements)
        {
            e.Update();
        }
        // 카메라 위치를 따라가는 데 움직일 수록 - 로 해야 뒤로 밀림.
        // 카메라를 자식으로 두면 흔들리는 스크립트 때 배경도 흔들림.
    }

    [System.Serializable]
    public class ParallaxElement
    {
        float tempTr;
        float timer;
        float dir;
        float dir2;
        public Parallax parallax;
        public GameObject[] GameObjects;
        [Range(0.0f, 100f)]
        public float SpeedRatio;

        public void Start()
        {
            //Parallax parallax = GetComponent<Parallax>();
        }

        public void Update()
        {
            foreach (GameObject obj in GameObjects)
            {
                if (timer >= 0.1f)
                {
                    dir = tempTr - parallax.cameraManager.transform.position.x;
                    timer = 0;
                }

                else if (timer >= 0)
                {
                    tempTr = parallax.cameraManager.transform.position.x;
                    timer += Time.deltaTime;
                }

                obj.transform.Translate( new Vector2 (dir * SpeedRatio * Time.deltaTime, 0.0f));
            }
        }
    }
}
