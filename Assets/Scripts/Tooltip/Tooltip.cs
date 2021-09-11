using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Text pointText;

    public void SetupTooltip(string _name, string _des, string _point)
    {
        nameText.text = _name;
        descriptionText.text = _des;
        pointText.text = _point;
    }

    float halfwidthX;
    float halfwidthY;
    float pivotX;
    float pivotY;
    RectTransform rt;
    private void Start()
    {
        halfwidthX = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        halfwidthY = GetComponentInParent<CanvasScaler>().referenceResolution.y * 0.5f;
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (rt.anchoredPosition.x + rt.sizeDelta.x > halfwidthX)
            pivotX = 1;
        else
            pivotX = 0;
        if (rt.anchoredPosition.y + rt.sizeDelta.y > halfwidthY)
            pivotY = 1;
        else
            pivotY = 0;

        rt.pivot = new Vector2(pivotX, pivotY);
    }
}
