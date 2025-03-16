using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;//画像を持つGameObject
    public Sprite gameOverSpr;//GameOver画像
    public Sprite gameClearSpr;//GameClear画像
    public GameObject panel;//パネル
    public GameObject restartButton;//Restartボタン
    public GameObject nextButton;//ネクストボタン

    Image titleImage;//画像を表示しているImageコンポーネント

    //--------時間制限追加-----------
    public GameObject timeBar;//時間表示イメージ
    public GameObject timeText;//時間テキスト
    TimeController timeCnt;//TimeController

    //---------スコア追加-----------
    public GameObject scoreText; //スコアテキスト
    public static int totalScore;//合計スコア
    public int stageScore = 0;//ステージスコア

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //画像を非表示にするメソッドを1秒後に呼ぶ
        Invoke("InactiveImage", 1.0f);
        //ボタン（パネル）を非表示にする
        panel.SetActive(false);

        //------------時間制限追加----------------
        //TimeControllerを取得
        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false); //制限時間なしなら隠す
            }
        }
        
        //---------スコア追加-----------
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            //ゲームクリア
            mainImage.SetActive(true);//画像を表示
            panel.SetActive(true);//ボタンを表示
            //RESTARTボタンを無効化
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;//ボタンを半透明の非アクティブ状態にする
            mainImage.GetComponent<Image>().sprite = gameClearSpr;//ゲームクリア画像を設定
            PlayerController.gameState = "gameend";

            //-------------時間制限追加----------
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;//時間カウント停止

                //-----------スコア追加-----------
                //整数に代入して小数を切り捨てる
                int time = (int)timeCnt.displayTime;
                totalScore += time*10;//残り時間をスコアに加算
            }

            //--------------スコア追加----------------
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();//スコア更新
        }
        else if (PlayerController.gameState == "gameover")
        {
            //ゲームオーバー
            mainImage.SetActive(true);//画像を表示
            panel.SetActive(true);//ボタンを表示
            //NEXTボタンを無効化
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;//ゲームオーバー画像を設定
            PlayerController.gameState = "gameend";

            //時間制限追加
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //時間カウント停止
            }
        }
        else if (PlayerController.gameState == "playing")
        {
            //Debug.Log("プレイ中");
            //ゲームプレイ中
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerControllerを取得する
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //時間制限追加
            //タイムを更新する
            if (timeCnt != null)
            {
                //Debug.Log("TimeController発見");
                if (timeCnt.gameTime > 0.0f)
                {
                    //Debug.Log("gameTimeが0より大きい");
                    //整数に代入することで小数点以下を切り捨てる
                    int time = (int)timeCnt.displayTime;
                    //タイム更新
                    timeText.GetComponent<Text>().text = time.ToString();
                    //タイムオーバー
                    if (time == 0)
                    {
                        Debug.Log("タイムオーバー");
                        playerCnt.GameOver(); //ゲームオーバーにする
                    }
                }
            }

            //-----------スコア追加--------------
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
    }

    //画像を非表示にするメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //スコア追加メソッド
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
