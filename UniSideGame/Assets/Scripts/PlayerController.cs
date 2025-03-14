using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;//Rigidbody2D型の変数
    float axisH = 0.0f;//入力
    public float speed = 3.0f;//移動速度

    public float jump = 9.0f;//ジャンプ初速
    public LayerMask groundLayer;//着地できるレイヤー
    bool goJump = false;//ジャンプ開始フラグ

    //アニメーション対応
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump"; 
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "playing";//ゲームの状態

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを取得する
        rbody=GetComponent<Rigidbody2D>();
        //Animatorを習得する
        animator = GetComponent<Animator>();
        //停止から開始
        nowAnime = stopAnime;
        //停止から開始
        oldAnime = stopAnime;

        gameState = "playing"; //ゲームの状態をゲームプレイ中にする
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームプレイ中でなければ更新しない
        if (gameState != "playing")
        {
            return;
        }

        //水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            //オブジェクトの形はそのまま
            transform.localScale = new Vector2(1,1);
        }
        else if (axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            //左右反転
            transform.localScale = new Vector2(-1,1);
        }

        

        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }


    void FixedUpdate()
    {
        //ゲームプレイ中でなければ更新しない
        if (gameState != "playing")
        {
            return;
        }

        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position, //発射位置
        0.2f, //円の半径
        Vector2.down, //発射方向
        0.0f, //発射距離
        groundLayer);//検出するレイヤー

        if (onGround){Debug.Log("接地");}

        if (onGround || axisH != 0)
        {
            //地上または速度が０でないなら
            //速度を更新する
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }
        if (onGround && goJump)
        {
            Debug.Log("ジャンプ");
            //地上でジャンプキーが押されたら
            //ジャンプさせる
            Vector2 jumpPW = new Vector2 (0,jump);//ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPW, ForceMode2D.Impulse);//瞬間的な力を加える
            goJump = false;//ジャンプフラグをおろす
        }
        
        if (onGround)
        {
            //地面の上
            if (axisH == 0)
            {
                nowAnime = stopAnime; //停止中
            }
            else
            {
                nowAnime = moveAnime; //移動
            }
        }
        else
        {
            //空中
            nowAnime = jumpAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    //ジャンプフラグを立てる
    void Jump()
    {       
        goJump = true;
    }

    //接触開始
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();//ゴール
        }
        else if(collision.gameObject.tag == "Dead")
        {
            GameOver();//ゲームオーバー
        }
    }

    //ゴールメソッド
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";//ゲームクリア状態
        GameStop();//ゲーム停止
    }
    //ゲームオーバーメソッド
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";//ゲームオーバー状態
        GameStop();//ゲーム停止

        /*
        ゲームオーバー演出
        */

        //プレイヤー当たりを消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        //プレイヤーを少し上に跳ね上げる演出
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
    }

    //ゲーム停止メソッド
    public void GameStop()
    {
        //Rigidbody2Dを取得する
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //速度を０にして強制停止
        rbody.linearVelocity = Vector2.zero;
    }
}

    
