using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Rigidbody2D plrb2d;//プレイヤーのRigidbody2D
    GameObject player;//プレイヤー
    PlayerController playerCnt;//プレイヤーコントローラー
    Vector2 playerPosition;//キャラから見たプレイヤーの位置
    float naiseki;
    float cosTheta;
    float theta;
    
    bool isMoving = false;//動いているかどうか
    Vector2 Direction;//自動移動時のゴール
    Vector2 targetPosition;//移動先

    float speed = 5.0f;//移動速度
    float gap;//ゴールまでの距離
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーのRigidbody2Dを取得
        plrb2d = player.GetComponent<Rigidbody2D>();
        //プレイヤーコントローラーを取得
        playerCnt = player.GetComponent<PlayerController>();
        //Rigidbody2Dを取得
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if (player != null)
        {
            if (!isMoving && playerCnt.isMoving)
            {
                StartCoroutine(Move());
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator Move()
    {
        //動いているフラグ立て
        isMoving = true;
        float gap = 1.0f;
        float isGoal = 0.05f;
        
        targetPosition = new Vector2(Mathf.Round(player.transform.position.x), Mathf.Round(player.transform.position.y));
        Direction = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).normalized;
        rb2d.linearVelocity = new Vector2(Direction.x * speed, Direction.y * speed);

        while(isGoal < gap)
        {
            Direction = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).normalized;
            Debug.Log(rb2d.linearVelocity);
            rb2d.linearVelocity = new Vector2(Direction.x * speed, Direction.y * speed);
            gap = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).magnitude;
            yield return null;
        }
        //動いているフラグおろし
        isMoving = false;
        //座標を格子点に
        transform.position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
    }
}
