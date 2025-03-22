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
        
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("イベントフラグオン");
        if (other.gameObject.tag == "Event")
        {
            //プレイヤーがイベント相手を見ていたら
            //イベントに入れる状態にする
            playerCnt.eventFlag = true;
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Event")
        {
            //プレイヤーがイベント相手から目をそらしたら
            //イベントに入れない状態にする
            playerCnt.eventFlag = false;
        }
    }
}
