using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Ability : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject abilityText;
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        dataManager.LoadData();
        if (dataManager.stage == 0)
        {
            abilityText.SetActive(true);
        }
        else
            abilityText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
