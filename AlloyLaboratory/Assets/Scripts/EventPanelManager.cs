using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventPanelManager : MonoBehaviour
{
    public bool isOnEvent;
    public Sprite image1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isOnEvent)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //テキストイベントの進行に合わせて表示画像を切り替えていく
        //Debug.Log(GameManager.eventProgress);
    }
}
