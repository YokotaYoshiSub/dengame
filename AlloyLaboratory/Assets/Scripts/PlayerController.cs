using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody2D rb2d;
    public float speed = 5.0f;//歩きスピード
    float axisH = 0.0f;//左右入力離散値
    float axisV = 0.0f;//上下入力離散値
    Vector2 inputVector;//入力方向
    float preAxisH = 0.0f;//axisHの一時保存
    float preAxisV = 0.0f;//axisVの一時保存

    public bool isMoving = false;//移動中かどうか
    public bool isCoroutineWorking = false;//コルーチン中かどうか
    public bool eventFlag = false;//イベントに入れる状態かどうか
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();//Rigidbody2Dの取得
    }

    // Update is called once per frame

    void Update()
    {       
        //Debug.Log(new Vector2(axisH, axisV));
        
        //左シフトでダッシュ状態
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        //ダッシュ解除
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5.0f;
        }

        //ベクトル(axisH, axisV)は(0,0),(+-1,0),(0,+-1)のいずれかである
        if (axisV == 0)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }
        if (axisH == 0)
        {
            axisV = Input.GetAxisRaw("Vertical");
        }
        inputVector = new Vector2(axisH, axisV);//入力ベクトル
        //Debug.Log(inputVector);

        //入力が1,-1から変更されたらコルーチン開始
        if (preAxisH != 0 && preAxisH != axisH)
        {
            if (isCoroutineWorking)
            {
                //すでにコルーチンが働いていたら、入力の保存だけして抜ける
                preAxisH = axisH;
                preAxisV = axisV;
                return;
            }
            StartCoroutine(Move(preAxisH, 0.0f));
            preAxisH = axisH;
            preAxisV = axisV;
        }
        else if (preAxisV != 0 && preAxisV != axisV)
        {
            if (isCoroutineWorking)
            {
                //すでにコルーチンが働いていたら、入力の保存だけして抜ける
                preAxisH = axisH;
                preAxisV = axisV;
                return;
            }
            StartCoroutine(Move(0.0f, preAxisV));
            preAxisH = axisH;
            preAxisV = axisV;
        }
        else
        {
            //入力を一時保存する
            preAxisH = axisH;
            preAxisV = axisV;
        }
    }

    void FixedUpdate()
    {
        
        if (!isMoving)
        {
            //動いていない状態なら
            //速度を更新
            rb2d.linearVelocity = new Vector2(speed*axisH, speed*axisV);

            if (axisH != 0 || axisV != 0)
            {
                //上下左右いずれかの入力があるなら、少なくとも動いている状態である
                isMoving = true;
            }
        }
        
    }
    
    //上下左右の入力終了後の自動運転
    private IEnumerator Move(float x, float y)
    {
        isCoroutineWorking = true;
        float distance = 0.0f;//プレイヤーの現在位置から格子点までの距離

        //格子点までの距離を計測
        if (x > 0.5f)
        {
            //右方向
            distance = Mathf.Ceil(transform.position.x)-transform.position.x;
        }
        else if (x < -0.5f)
        {
            //左方向
            distance = transform.position.x - Mathf.Floor(transform.position.x);
        }
        else if (y > 0.5f)
        {
            //上方向
            distance = Mathf.Ceil(transform.position.y)-transform.position.y;
        }
        else if (y < -0.5f)
        {
            //下方向
            distance = transform.position.y - Mathf.Floor(transform.position.y);
        }
        //格子点にいたるまでの時間
        //直前までダッシュしていたらダッシュ、していなかったら歩き
        float time = distance / speed;
        //格子点にいたるまで待つ
        yield return new WaitForSeconds(time);
        //格子点についたら座標を整数値にし、速度を0にし、動いていない状態にする
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        rb2d.linearVelocity = Vector2.zero;
        isMoving = false;
        isCoroutineWorking = false;
    }
}
    
    