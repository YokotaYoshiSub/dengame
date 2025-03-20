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
    
    bool isMoving = false;//動いているかどうか
    bool isCoroutineWorking = false;//コルーチン中かどうか
    Vector2 Direction;//自動移動時のゴール
    Vector2 targetPosition;//移動先

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
        if (player != null)
        {

        }
        /*
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
        Debug.Log(Mathf.Rad2Deg*theta);
        */
        
    }

    void FixedUpdate()
    {
        /*
        if (plrb2d.linearVelocity.magnitude < 0.01f)
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
            if (theta < 0.1f)
            {

                rb2d.linearVelocity = plrb2d.linearVelocity;
            }
            //移動方向が交差ならコルーチン始動
            else if (theta<3f)
            {
                if (!isCoroutineWorking)
                {
                    Debug.Log("開始");
                    StartCoroutine(SideMove());
                }
            }
            //移動方向が真逆なら
            else
            {
                {
                    //StartCoroutine(OppoMove());
                }
            }
        }*/
    }

    IEnumerator Move()
    {
        //動いているフラグ立て
        isMoving = true;
        float time = 0.0f;
        targetPosition = new Vector2(Mathf.Round(player.transform.position.x), Mathf.Round(player.transform.position.y));
        while()
        {
            yield return null;
        }
        //動いているフラグおろし
        isMoving = false;
        //座標を格子点に
        transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
    }

    IEnumerator SideMove()
    {
        isCoroutineWorking = true;//コルーチン中
        //移動方向を設定
        Direction = new Vector2(Mathf.Round(player.transform.position.x)-transform.position.x, Mathf.Round(player.transform.position.y)-transform.position.y);
        //コルーチン時間計算
        float time = Direction.magnitude/speed;
        
        
        yield return new WaitForSeconds(time-0.01f);//待機
        //座標を格子点に
        transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
        isCoroutineWorking = false;//コルーチン終わり
    }
}
