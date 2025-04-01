using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //------------------左端の情報パネル----------------------
    public GameObject informationPanel;//左端の情報パネル
    public Image charaIcon;//操作キャラクターのアイコン
    public Sprite chara1;//誰かのアイコン画像
    //hp処理
    public Image hp1;
    public Image hp2;
    public Image hp3;
    //----------------下端のテキストパネル---------------------

    public GameObject textPanel;//下端のテキストパネル
    public Image speakerIcon;//話者アイコン
    public Image textBox;//テキストボックス
    public GameObject nameText;//名前
    public GameObject chatText;//文章
    int chatNum = 0;//会話の何番目のテキストか
    //-----------------ゲームオーバー処理---------------------
    public GameObject gameOverPanel;
    //--------------------その他-------------------------

    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    GameObject playerFocus;//プレイヤーの目線
    PlayerFocus playerFocusCS;//PlayerFocusスクリプト
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textPanel.SetActive(false);//最初はテキストボックスは非表示
        gameOverPanel.SetActive(false);//最初はゲームオーバー画面を見せない

        player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを取得
        playerCnt = player.GetComponent<PlayerController>();//プレイヤーコントローラーを取得
        playerFocus = GameObject.FindGameObjectWithTag("PlayerFocus");//プレイヤーの目線を取得
        playerFocusCS = playerFocus.GetComponent<PlayerFocus>();//PlayerFocusスクリプト取得
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------会話イベント-------------------------------
        if (playerFocusCS.eventFlag == true)
        {
            //プレイヤーの目線がキャラクターに重なっているとき
            if (chatNum == 0)
            {
                //会話が0番目＝まだ会話していない状態なら
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    Time.timeScale = 0;//ゲームストップ
                    textPanel.SetActive(true);//テキストボックス表示
                    nameText.GetComponent<Text>().text = playerFocusCS.people[0];//配列の1番目の名前を表示
                    chatText.GetComponent<Text>().text = playerFocusCS.texts[0];//配列の1番目のテキストを表示
                    chatNum = 1;//1番目の会話終了
                }
            }
            else if (chatNum >= playerFocusCS.textNum)
            {
                //配列の最後の会話が終了したら
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    Time.timeScale = 1;//ゲーム再開
                    nameText.GetComponent<Text>().text = null;//名前をなにもなしに
                    chatText.GetComponent<Text>().text = null;//テキストをなにもなしに
                    textPanel.SetActive(false);//テキストボックス非表示
                    chatNum = 0;//会話していない状態に変更
                }
            }
            else if (chatNum >= 1)
            {
                //n回会話している＝テキストボックスが表示されているなら
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    nameText.GetComponent<Text>().text = playerFocusCS.people[chatNum];//配列のn+1番目の名前を表示
                    chatText.GetComponent<Text>().text = playerFocusCS.texts[chatNum];//配列のn+1番目のテキストを表示
                    chatNum += 1;//n+1回目の会話終了
                }
            }
        }

        //--------------------------体力処理---------------------------
        if (PlayerController.hp >= 3)
        {
            hp1.gameObject.SetActive(true);
            hp2.gameObject.SetActive(true);
            hp3.gameObject.SetActive(true);
        }
        else if (PlayerController.hp == 2)
        {
            hp1.gameObject.SetActive(true);
            hp2.gameObject.SetActive(true);
            hp3.gameObject.SetActive(false);
        }
        else if (PlayerController.hp == 1)
        {
            hp1.gameObject.SetActive(true);
            hp2.gameObject.SetActive(false);
            hp3.gameObject.SetActive(false);
        }
        else 
        {
            hp1.gameObject.SetActive(false);
            hp2.gameObject.SetActive(false);
            hp3.gameObject.SetActive(false);
        }

        //-------------------------ゲームオーバー処理----------------------------
        if (PlayerController.hp <= 0)
        {
            //プレイヤーの体力が0を下回ったら
            Invoke("GameOver", 0.5f);
        }
    }

    //ゲームオーバーメソッド
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
