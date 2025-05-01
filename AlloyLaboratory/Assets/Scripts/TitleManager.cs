using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject saveDatasPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveDatasPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSaveDatasPanel()
    {
        saveDatasPanel.SetActive(true);
    }
}
