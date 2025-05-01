using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UIを担当
    //float debugTime = 0;//デバッグ用のタイマー
    public string gameState = "playing";
    //------------------左端の情報パネル----------------------
    public GameObject informationPanel;//左端の情報パネル
    public Image charaIcon;//操作キャラクターのアイコン
    public Sprite chara1;//誰かのアイコン画像
    public bool isPanelOn = true;//パネルを表示するかどうか
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
    //float textOrderingTime;//テキストを表示させる時間
    bool isTextDisplaying = false;//テキスト表示中かどうか
    bool isTextComposing = false;//テキストが生成中かどうか
    
    //---------------------メニューパネル
    public GameObject menuPanel;
    //--------------------その他-------------------------

    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    GameObject playerFocus;//プレイヤーの目線
    PlayerFocus playerFocusCS;//PlayerFocusスクリプト
    //イベントのフラグ
    public static int eventProgress = 0;//この数値を切り替えることでイベント進行
    float debugTime = 0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textPanel.SetActive(false);//最初はテキストボックスは非表示
        menuPanel.SetActive(false);//最初は表示しない

        if (isPanelOn)
        {
            informationPanel.SetActive(true);//情報パネル表示
        }
        else
        {
            informationPanel.SetActive(false);//情報パネル非表示
        }

        //eventProgressの値によって操作キャラの画像を帰る
        if (eventProgress <= 100)
        {
            charaIcon.sprite = chara1;
        }

        player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを取得
        playerCnt = player.GetComponent<PlayerController>();//プレイヤーコントローラーを取得
        playerFocus = GameObject.FindGameObjectWithTag("PlayerFocus");//プレイヤーの目線を取得
        playerFocusCS = playerFocus.GetComponent<PlayerFocus>();//PlayerFocusスクリプト取得
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(eventProgress);

        //---------------------------会話イベント-------------------------------

        //Debug.Log(chatNum);


        if (playerCnt.onEvent == true)
        {
            //シーン切り替え直後のイベント
            if (chatNum == 0)
            {
                textPanel.SetActive(true);//テキストボックス表示
                //nameText.GetComponent<Text>().text = playerFocusCS.people[0];//配列の1番目の名前を表示
                
                if (!isTextComposing && !isTextDisplaying)
                {
                    StartCoroutine(TextFlow(playerFocusCS.texts[0]));//配列の1番目のテキストを表示

                    
                }
                if (!isTextComposing && isTextDisplaying)
                {
                    //テキスト生成終わりかつ
                    //テキスト表示中
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        chatNum = 1;//1番目の会話終了
                        isTextDisplaying = false;//次のテキストを生成できるように
                    }
                }
                
            }
            else if (chatNum >= playerFocusCS.textNum)
            {
                //配列の最後の会話が終了したら
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    Time.timeScale = 1;//ゲーム再開
                    playerCnt.onEvent = false;
                    nameText.GetComponent<Text>().text = null;//名前をなにもなしに
                    chatText.GetComponent<Text>().text = null;//テキストをなにもなしに
                    textPanel.SetActive(false);//テキストボックス非表示
                    chatNum = 0;//会話していない状態に変更
                    //テキスト生成できるように
                    isTextDisplaying = false;
                    isTextComposing = false;
                }
            }
            else if (chatNum >= 1)
            {
                //n回会話している＝テキストボックスが表示されているなら
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    nameText.GetComponent<Text>().text = playerFocusCS.people[chatNum];//配列のn+1番目の名前を表示

                    if (!isTextComposing && !isTextDisplaying)
                    {
                        StartCoroutine(TextFlow(playerFocusCS.texts[chatNum]));//配列のn+1番目のテキストを表示
                    }
                    
                }

                if (isTextDisplaying && !isTextComposing)
                {
                    chatNum += 1;//n+1回目の会話終了
                    isTextDisplaying = false; //次のテキストを生成できるように
                }
            }

        }
        


        if (playerFocusCS.eventFlag == true)
        {
            //プレイヤーの目線がキャラクターに重なっているとき
            if (chatNum < playerFocusCS.textNum)
            {
                //会話途中
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    //Debug.Log(chatNum);
                    //Enterキーを押すと
                    Time.timeScale = 0;//ゲームストップ
                    textPanel.SetActive(true);//テキストボックス表示
                    nameText.GetComponent<Text>().text = playerFocusCS.people[chatNum];//配列の1番目の名前を表示
                    
                    if (!isTextDisplaying && !isTextComposing)
                    {
                        //テキスト非表示かつ
                        //テキスト生成が進んでいなかったら

                        StartCoroutine(TextFlow(playerFocusCS.texts[chatNum]));
                    }
                }

                if (isTextDisplaying && !isTextComposing)
                {
                    //テキスト表示中で
                    //テキスト生成が終わったら
                    chatNum += 1;//1番目の会話終了
                    isTextDisplaying = false;//つぎのテキストを生成できるように
                }
            }
            else
            {
                //配列の最後の会話が終了したら
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Enterキーを押すと
                    Time.timeScale = 1;//ゲーム再開
                    nameText.GetComponent<Text>().text = null;//名前をなにもなしに
                    chatText.GetComponent<Text>().text = null;//テキストをなにもなしに
                    textPanel.SetActive(false);//テキストボックス非表示
                    eventProgress += playerFocusCS.eventProgressGetPoint;//ものによってはイベント進行
                    chatNum = 0;//会話していない状態に変更
                }
            }
        }
        //アイテムを取得した時

        //------------------まだ先に進めないところに行こうとしたときに引き留める--------------------------
        if (playerFocusCS.isPrevented)
        {
            Time.timeScale = 0;//ゲームストップ
            textPanel.SetActive(true);//テキストボックス表示
            nameText.GetComponent<Text>().text = playerFocusCS.people[0];//配列の1番目の名前を表示
            //debugTime += Time.deltaTime;

            //StartCoroutine(TextFlow(playerFocusCS.texts[0]));
            
            
            if (!isTextDisplaying && !isTextComposing)
            {
                //テキスト非表示かつ
                //テキスト生成が進んでいなかったら

                StartCoroutine(TextFlow(playerFocusCS.texts[0]));
            }
            
            

            if(isTextDisplaying && !isTextComposing && Input.GetKeyDown(KeyCode.Return))
            {
                //テキスト表示中で
                //テキストの生成は終わった状態で
                //Enterキーを押すと
                
                Time.timeScale = 1;//ゲーム再開
                nameText.GetComponent<Text>().text = null;//名前をなにもなしに
                chatText.GetComponent<Text>().text = null;//テキストをなにもなしに
                textPanel.SetActive(false);//テキストボックス非表示
                isTextDisplaying = false;//テキストが非表示になった
                playerFocusCS.isPrevented = false;//動かせる状態に
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
            //体力0=ゲームオーバー
            debugTime += Time.deltaTime;
            Debug.Log(debugTime);
            hp1.gameObject.SetActive(false);
            hp2.gameObject.SetActive(false);
            hp3.gameObject.SetActive(false);
            Invoke("GameOver", 1.0f);
            gameState = "gameOver";
        }
    }

    void FixedUpdate()
    {
        //Debug.Log(Time.deltaTime);
    }

    //ゲームオーバーメソッド
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    //-----------------------テキスト送りコルーチン
    IEnumerator TextFlow(string text)
    {
        //0.02sにつき1文字ずつ表示させたい
        //そのうえでEnterでスキップ、一瞬で表示
        //全部表示させてからEnterでテキスト非表示
        isTextDisplaying = true;
        isTextComposing = true;
        float textOrderingTime = 0f;

        chatText.GetComponent<Text>().text = text.Substring(0);

        while (true)
        {
            textOrderingTime += Time.unscaledDeltaTime;
            //Debug.Log(textOrderingTime);
            
            for (int i = 0; i <= text.Length; i++)
            {
                if (textOrderingTime > 0.1f * (float)i)
                {
                    chatText.GetComponent<Text>().text = text.Substring(0, i);
                }
            }
            
            
            yield return null;

            if (textOrderingTime >= (float)text.Length*0.1f)
            {
                break;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                break;
            }
        }

        chatText.GetComponent<Text>().text = text;
        isTextComposing = false;
    }

}
