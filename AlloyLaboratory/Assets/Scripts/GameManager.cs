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
    public GameObject chatText;//文章

    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    GameObject playerFocus;//プレイヤーの目線
    PlayerFocus playerFocusCS;//PlayerFocusスクリプト
    int chatNum = 0;//会話の何番目のテキストか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextPanel.SetActive(false);//最初はテキストボックスは非表示
        player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを取得
        playerCnt = player.GetComponent<PlayerController>();//プレイヤーコントローラーを取得
        playerFocus = GameObject.FindGameObjectWithTag("PlayerFocus");//プレイヤーの目線を取得
        playerFocusCS = playerFocus.GetComponent<PlayerFocus>();//PlayerFocusスクリプト取得
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFocusCS.eventFlag == true)
        {
            if (chatNum == 0)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    TextPanel.SetActive(true);//テキストボックス表示
                    
                    chatText.GetComponent<Text>().text = playerFocusCS.texts[0];
                    chatNum = 1;
                }
            }
            else if (chatNum >= playerFocusCS.textNum)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    chatText.GetComponent<Text>().text = null;
                    TextPanel.SetActive(false);
                    chatNum = 0;
                }
            }
            else if (chatNum >= 1)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    chatText.GetComponent<Text>().text = playerFocusCS.texts[chatNum];
                    chatNum += 1;
                }
            }

            
        }
    }
}
