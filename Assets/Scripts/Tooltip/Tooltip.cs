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

    float halfwidth;
    RectTransform rt;
    private void Start()
    {
        halfwidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (rt.anchoredPosition.x + rt.sizeDelta.x > halfwidth)
            rt.pivot = new Vector2(1, 1);
        else
            rt.pivot = new Vector2(0, 1);
    }
}
