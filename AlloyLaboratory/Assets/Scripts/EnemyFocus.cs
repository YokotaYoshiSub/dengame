using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFocusCS : MonoBehaviour
{
    public string parent = "Damage1";//どの敵の子か
    GameObject enemy;//敵のゲームオブジェクト
    EnemyChaseController enemyChaseCnt;//敵のスクリプト
    Rigidbody2D enemyRb2d;//敵の物理挙動
    float offset = 0.5f;//敵の中心からの距離

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag(parent);//敵のゲームオブジェクトを取得
        enemyChaseCnt = enemy.GetComponent<EnemyChaseController>();//敵のスクリプトを取得
        enemyRb2d = enemy.GetComponent<Rigidbody2D>();//敵の物理挙動を取得
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyChaseCnt.playerDirectionDegree);
        //座標をプレイヤーの方向に向ける
        //実際に動く方向を決定
        if (enemyChaseCnt.playerDirectionDegree >= -50 && enemyChaseCnt.playerDirectionDegree < 50)
        {
            //プレイヤーが右のほうにいる
            //敵の右に移動
            transform.position = new Vector2(enemy.transform.position.x + offset, enemy.transform.position.y);
        }
        else if (enemyChaseCnt.playerDirectionDegree >= 50 && enemyChaseCnt.playerDirectionDegree < 130)
        {
            //プレイヤーが上のほうにいる
            //敵の敵の上に移動
            transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + offset);
        }
        else if (enemyChaseCnt.playerDirectionDegree >= -130 && enemyChaseCnt.playerDirectionDegree < -50)
        {
            //プレイヤーが下のほうにいる
            //敵の敵の下に移動
            transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y - offset);
        }
        else
        {
            //プレイヤーが左のほうにいる
            //敵の左に移動
            transform.position = new Vector2(enemy.transform.position.x - offset, enemy.transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            //プレイヤー以外のオブジェクトが目の前にあることを通知
            enemyChaseCnt.isBlocked = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            //目の前が開けている
            enemyChaseCnt.isBlocked = false;
        }
    }
}
