using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    //--------------------------プレイヤー関係------------------------------------------------
    GameObject player;
    PlayerController playerCnt;
    Vector2 playerPosition;
    float playerDistance;

    //---------------------------自分関係------------------------------------------------------
    Rigidbody2D rb2d;
    bool isAttacking = false;
    public float dashAttackSpeed = 5f;//ダッシュ攻撃の速度
    public GameObject attackArea;
    float attackCoolTime = 3f;
    float attackReadyTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを取得
        playerCnt = player.GetComponent<PlayerController>();//プレイヤーコントローラー取得
        rb2d = GetComponent<Rigidbody2D>();//Rigidbody2Dを取得
    }

    // Update is called once per frame
    void Update()
    {
        //自分からみたプレイヤーの位置を取得
        playerPosition = new Vector2(player.transform.position.x - transform.position.x,
        player.transform.position.y - transform.position.y);
        //プレイヤーとの距離を取得
        playerDistance = playerPosition.magnitude;

        attackReadyTime += Time.deltaTime;

        if (playerDistance <= 2f)
        {
            //プレイヤーと接近したら
            //攻撃判定を出してプレイヤーを攻撃する
            if (attackReadyTime >= attackCoolTime)
            {
                Instantiate(attackArea, transform.position, Quaternion.identity);
                attackReadyTime = 0;//攻撃準備状態に
            }
            
        }

        if (playerDistance >= 10f)
        {
            //プレイヤーから離れたら
            //レーザー打つか、ミサイル飛ばすかする
        }

        //障害となるブロックを破壊しながら進みたい
    }

    void FixedUpdate()
    {
        if (isAttacking == false)
        {
            if (playerPosition.x >= -1 && playerPosition.x <= 1)
            {
                if (playerPosition.y > 0)
                {
                    //プレイヤーが真上にいるとき
                    StartCoroutine(DashAttack(0f, 1f));
                }
                else
                {
                    //プレイヤーが真下にいるとき
                    StartCoroutine(DashAttack(0f, -1f));
                }
            }
        
            if (playerPosition.y >= -1 && playerPosition.y <= 1)
            {
                if (playerPosition.x > 0)
                {
                    //プレイヤーが真右にいるとき
                    StartCoroutine(DashAttack(1f, 0f));
                }
                else
                {
                    //プレイヤーが真左にいるとき
                    StartCoroutine(DashAttack(-1f, 0f));
                }
            }
        }
        
    }

    //プレイヤーに接近すると攻撃

    //プレイヤーが飛車の移動範囲にいるとき、突撃攻撃
    //突撃開始時のプレイヤーの位置より少し行き過ぎる。
    IEnumerator DashAttack(float x, float y)
    {
        //Vector2 attackDirection = new Vector2(x, y);

        isAttacking = true;
        float attackTime = 0;
        float moveDistance = playerDistance;

        while(true)
        {
            attackTime += Time.deltaTime;

            rb2d.linearVelocity = new Vector2(x * dashAttackSpeed, y * dashAttackSpeed);
            

            if (attackTime >= moveDistance / dashAttackSpeed + 0.5f)
            {
                //ちょっと通り過ぎて
                break;
            }

            yield return null;
        }

        //速度をゼロに
        rb2d.linearVelocity = Vector2.zero;
        //攻撃終わり
        isAttacking = false;
        //最後に格子点に移動
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
}
