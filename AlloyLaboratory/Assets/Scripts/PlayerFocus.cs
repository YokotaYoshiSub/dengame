using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{

    float axisH = 0.0f;
    float axisV = 0.0f;
    float positionX = 0.0f;
    float positionY = 0.0f;
    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    EventController eventCnt;//イベントコントローラー
    public bool eventFlag = false;//会話イベントに入れる状態かどうか
    public int textNum;//テキスト数
    public string[] texts;//会話テキスト
    public string[] people;//会話の話者

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーコントローラーを取得
        playerCnt = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------位置についての記述-------------------------------
        //ベクトル(axisH, axisV)は(0,0),(+-1,0),(0,+-1)のいずれか
        if (axisV == 0)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }
        if (axisH == 0)
        {
            axisV = Input.GetAxisRaw("Vertical");
        }
        //直前の入力を保存
        if (axisH == 1.0f)
        {
            positionX = 1.0f;
            positionY = 0.0f;
        }
        else if (axisH == -1.0f)
        {
            positionX = -1.0f;
            positionY = 0.0f;
        }
        else if (axisV == 1.0f)
        {
            positionX = 0.0f;
            positionY = 1.0f;
        }
        else if (axisV == -1.0f)
        {
            positionX = 0.0f;
            positionY = -1.0f;
        }
        //座標はプレイヤーの見ている方向
        transform.position = new Vector2(player.transform.position.x + positionX, player.transform.position.y + positionY); 
        //Debug.Log(transform.position);
        
        //------------------------------------イベントについての記述----------------------------------
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("イベントフラグオン");
        if (other.gameObject.tag == "Event")
        {
            //プレイヤーがイベント相手を見ていたら
            //イベントに入れる状態である
            eventFlag = true;
            //対象のイベントコントローラーを取得
            eventCnt = other.GetComponent<EventController>();
            //テキスト数を取得
            textNum = eventCnt.textNum;
            //texts配列を初期化
            texts = new string[textNum];
            //テキスト情報を配列に収納
            for (int i = 0; i < textNum; i++)
            {
                texts[i] = eventCnt.texts[i];
                //Debug.Log(texts[i]);
            }
            //people配列を初期化
            people = new string[textNum];
            //テキスト情報を配列に収納
            for (int i = 0; i < textNum; i++)
            {
                people[i] = eventCnt.people[i];
                //Debug.Log(texts[i]);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Event")
        {
            //プレイヤーがイベント相手から目をそらしたら
            //イベントに入れない状態にする
            eventFlag = false;
        }
        //取得したイベントコントローラーを捨てる
        eventCnt = null;
    }
}
