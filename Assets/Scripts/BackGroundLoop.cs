using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    [Header("���ȭ��")]
    public GameObject background;
    public GameObject[] bgs;

    public bool isLoop;

    float speed = -2f;
    float endPos = -28.8f; // �Ѻ��� ���Ƽ� ���� ����̴ϱ� �ʱ� ��ġ�� ���� ������ �ȴ�.
    bool isIns = false;

    void Start()
    {
        bgs = new GameObject[2];
        bgs[0] = Instantiate(background, new Vector3(28.8f, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoop)
        {
            bgs[0].transform.Translate(speed * Time.deltaTime, 0, 0);
            if (bgs[0].transform.position.x <= endPos)
            {
                if (!isIns)
                {
                    bgs[1] = Instantiate(background, new Vector3(48f, 0, 0), Quaternion.identity);
                    isIns = true;
                }
                bgs[1].transform.Translate(speed * Time.deltaTime, 0, 0);

                if (bgs[0].transform.position.x <= -48.0)
                {
                    Destroy(bgs[0]);
                    bgs[0] = bgs[1];
                    bgs[1] = null;

                    isIns = false;
                }
            }
        }
    }
}
