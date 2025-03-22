using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject InformationPanel;//左端の情報パネル
    public Image CharaIcon;//操作キャラクターのアイコン
    public Sprite Chara1;//誰かのアイコン画像

    public GameObject TextPanel;//下端のテキストパネル
    public Image SpeakerIcon;//話者アイコン
    public Image TextBox;//テキストボックス
    public Text text;//文章

    public GameObject player;
    public PlayerController playerCnt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextPanel.SetActive(false);//最初はテキストボックスは非表示
        player = GameObject.FindGameObjectWithTag("Player");
        playerCnt = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCnt.eventFlag == true)
        {
            
            if(Input.GetKeyDown(KeyCode.Return))
            {
                TextPanel.SetActive(true);//テキストボックス表示
            }
        }
    }
}
