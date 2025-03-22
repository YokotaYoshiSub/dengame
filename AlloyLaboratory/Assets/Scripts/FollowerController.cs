using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Rigidbody2D playerRb2d;//プレイヤーのRigidbody2D
    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    Vector2 playerPosition;//キャラから見たプレイヤーの位置
    
    bool isMoving = false;//動いているかどうか
    Vector2 targetDirection;//自動移動時のゴールの方向
    Vector2 targetPosition;//移動先
    float distance;//プレイヤーとの距離

    float speed = 5.0f;//移動速度
    float gap;//ゴールまでの距離
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーのRigidbody2Dを取得
        playerRb2d = player.GetComponent<Rigidbody2D>();
        //プレイヤーコントローラーを取得
        playerCnt = player.GetComponent<PlayerController>();
        //Rigidbody2Dを取得
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        

        //プレイヤーとの距離を計測
        distance = new Vector2(player.transform.position.x - transform.position.x, 
        player.transform.position.y - transform.position.y).magnitude;

        
        if (distance < 1.1f)
        {
            //普通の状態
            //ダッシュ状態
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 10.0f;
            }
            //ダッシュ解除
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 5.0f;
            }
        }
        else if (distance < 2.0f)
        {
            //プレイヤーから離れたら速くして追いつかせる
            //ダッシュ状態
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 12.0f;
            }
            //ダッシュ解除
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 6.0f;
            }
        }
        else
        {
            //プレイヤーと離れすぎたら強制的に近づける
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1.0f);
        }

        
    }

    void FixedUpdate()
    {
        //プレイヤーを取得出来たら
        if (player != null)
        {
            if (playerRb2d.linearVelocity.magnitude < 4f)
            {
                return;
            }
            if (!isMoving && playerCnt.isMoving)
            {
                //プレイヤーに追従する
                //Debug.Log("移動はじめ");
                StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        //動いているフラグ立て
        isMoving = true;
        float gap = 1.0f;//毎フレーム更新する、ゴールまでの距離
        float isGoal = 0.1f;//ゴールまでの距離がこれ以下だったらゴールとする
        
        //ゴールはプレイヤーの座標に最も近い格子点
        targetPosition = new Vector2(Mathf.Round(player.transform.position.x), Mathf.Round(player.transform.position.y));
        //ゴールの方向の正規ベクトル
        targetDirection = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).normalized;
        //速度を設定
        if (playerRb2d.linearVelocity.magnitude >= 0.1f)
        {
            rb2d.linearVelocity = new Vector2(targetDirection.x * speed, targetDirection.y * speed);
        }
        

        while(isGoal <= gap)
        {
            //ゴールまでの距離が一定以下なら以下の処理を毎フレーム行う

            if (playerRb2d.linearVelocity.magnitude < 0.1f)
            {
                //プレイヤーが止まっていたら
                //動いているフラグおろし
                isMoving = false;
                //速度をゼロに
                rb2d.linearVelocity = Vector2.zero;
                //座標を格子点に
                transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
                //コルーチン停止
                yield break;
            }
            
            //ゴールの方向の正規ベクトルを更新
            targetDirection = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).normalized;
            //速度を更新し、ダッシュに対応する
            rb2d.linearVelocity = new Vector2(targetDirection.x * speed, targetDirection.y * speed);
            //ゴールまでの距離を更新
            gap = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).magnitude;
            
            yield return null;
        }
        //動いているフラグおろし
        isMoving = false;
        //速度をゼロに
        rb2d.linearVelocity = Vector2.zero;
        //座標を格子点に
        transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
    }
}
