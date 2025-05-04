using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuPanelManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("a");
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("1");
            gameObject.SetActive(true);
        }
    }
}
