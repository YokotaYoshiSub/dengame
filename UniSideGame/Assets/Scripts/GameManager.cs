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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //画像を非表示にするメソッドを1秒後に呼ぶ
        Invoke("InactiveImage", 1.0f);
        //ボタン（パネル）を非表示にする
        panel.SetActive(false);
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
        }
        else if (PlayerController.gameState == "playing")
        {
            //ゲームプレイ中
        }
    }

    //画像を非表示にするメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
