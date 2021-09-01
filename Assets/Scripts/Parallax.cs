using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float pixelsPerUnit;
    public bool FixJitters;
    [Tooltip("메인 카메라 안에 있는 카메라 매니저")]
    public CameraManager cameraManager;
    public float speed;
    [SerializeField]
    public ParallaxElement[] Elements;
    PlayerMovement pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
       // transform.position = new Vector2(cameraManager.transform.position.x, cameraManager.transform.position.y - 2f);
        //transform.position = Vector2.Lerp(transform.position, new Vector2(cameraManager.desiredPosition.x, cameraManager.desiredPosition.y - 1f), speed * Time.deltaTime);


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
        float dir;
        float scrolloffsetX;
        float timer;
        public Parallax parallax;
        public GameObject[] GameObjects;
        [Range(0.0f, 100f)]
        public float SpeedRatio;

        public void Update()
        {
            Debug.Log(timer);
            foreach (GameObject obj in GameObjects)
            {
                if (timer >= 0.02f)
                {
                    dir = tempTr - parallax.cameraManager.desiredPosition.x;
                    timer = 0;
                }

                else if (timer >= 0f)
                {
                    tempTr = parallax.cameraManager.desiredPosition.x;
                    timer += Time.fixedDeltaTime;
                }
                //dir = parallax.cameraManager.desiredPosition.x;
                obj.transform.Translate(new Vector2(Mathf.Round(dir / .1f) * .1f * SpeedRatio * Time.fixedDeltaTime, 0.0f));

                //obj.transform.position = obj.transform.position + new Vector3((dir * SpeedRatio * Time.deltaTime) + scrolloffsetX, 0 * Time.deltaTime, 0);

                if (parallax.FixJitters)
                {
                    Vector3 oldPos = obj.transform.localPosition;

                    int i_x = Mathf.FloorToInt(obj.transform.localPosition.x * (float)parallax.pixelsPerUnit);
                    int i_y = 0;
                    //int i_y = Mathf.FloorToInt(obj.transform.localPosition.y * (float)parallax.pixelsPerUnit);
                    //int i_z = Mathf.FloorToInt(obj.transform.localPosition.z * (float)parallax.pixelsPerUnit);
                    int i_z = 0;

                    Vector3 p = new Vector3((float)i_x / (float)parallax.pixelsPerUnit, (float)i_y / (float)parallax.pixelsPerUnit, (float)i_z / (float)parallax.pixelsPerUnit);

                    scrolloffsetX = oldPos.x - p.x;

                    obj.transform.localPosition = p;
                }
            }
        }
    }
}
