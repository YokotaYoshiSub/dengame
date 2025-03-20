using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Rigidbody2D plrb2d;//プレイヤーのRigidbody2D
    GameObject player;//プレイヤー
    Vector2 playerPosition;//キャラから見たプレイヤーの位置
    float naiseki;
    float cosTheta;
    float theta;
    bool isSameDirection = false;//プレイヤーの移動方向と一致しているかどうか
    bool isCoroutineWorking = false;//コルーチン中かどうか
    Vector2 targetPosition;//自動移動時のゴール

    float speed = 0.0f;//移動速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーのRigidbody2Dを取得
        plrb2d = player.GetComponent<Rigidbody2D>();
        //Rigidbody2Dを取得
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //キャラクターの移動先をプレイヤーの現在位置に設定
        if (player != null)
        {
            //キャラクターから見たプレイヤーの位置
            playerPosition = new Vector2
            (player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);
            //Debug.Log("プレイヤー発見");
            if (plrb2d.linearVelocity.magnitude != 0)
            {
                //Debug.Log("プレイヤー移動中");
                speed = player.GetComponent<PlayerController>().speed;  
            }
            else
            {
                speed = 0.0f;
            }
        }
        //内積から角度を求める
        if (playerPosition.magnitude != 0 && plrb2d.linearVelocity.magnitude != 0)
        {
            naiseki = playerPosition.x * plrb2d.linearVelocity.x + playerPosition.y * plrb2d.linearVelocity.y;
            cosTheta = naiseki/playerPosition.magnitude/plrb2d.linearVelocity.magnitude;
            theta = Mathf.Acos(cosTheta);
        }
        else 
        {
            theta = 0;
        }
        
        //Debug.Log(Mathf.Rad2Deg*theta);
        if (theta < 0.1)
        {
            isSameDirection = true;
        }
        else
        {
            isSameDirection = false;
        }
    }

    void FixedUpdate()
    {
        if (plrb2d.linearVelocity.magnitude < 0.1f)
        {
            //プレイヤーの移動速度がほぼ0なら
            rb2d.linearVelocity = Vector2.zero;//こっちも0にする
            transform.position = new Vector2
            (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));//座標を格子点に
        }
        else
        {
            //プレイヤーが移動中なら
            //移動方向が一致しているなら
            if (isSameDirection)
            {

                rb2d.linearVelocity = plrb2d.linearVelocity;
            }
            //移動方向が不一致ならコルーチン始動
            if (!isSameDirection && !isCoroutineWorking)
            {
                Debug.Log("開始");
                StartCoroutine(Move());
            }
            
        }
    }

    IEnumerator Move()
    {
        isCoroutineWorking = true;//コルーチン中
        float time = playerPosition.magnitude/speed;//コルーチン時間計算
        //コルーチン中の速度計算
        rb2d.linearVelocity = new Vector2(speed * Mathf.Round(playerPosition.x), speed * Mathf.Round(playerPosition.y));
        yield return new WaitForSeconds(time);//待機
        //座標を格子点に
        transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
        isCoroutineWorking = false;//コルーチン終わり
    }
}
