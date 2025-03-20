using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    //移動
    private Rigidbody2D rb2d;
    public float speed = 5.0f; 
    float axisH = 0.0f;
    float axisV = 0.0f;
    float preAxisH = 0.0f;
    float preAxisV = 0.0f;
    float Distance = 0.0f;

    bool isMoving = false;//移動中かどうか

    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {       
        //Debug.Log(new Vector2(axisH, axisV));
        
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

        //ベクトル(axisH, axisV)は(0,0),(+-1,0),(0,+-1)のいずれかである
        if (axisV == 0)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }
        if (axisH == 0)
        {
            axisV = Input.GetAxisRaw("Vertical");
        }

        //入力が変更されたらコルーチン開始
        if (preAxisH != axisH)
        {
            StartCoroutine(Move(preAxisH, 0.0f));
        }
        if (preAxisV != axisV)
        {
            StartCoroutine(Move(0.0f, preAxisV));
        }

        if (axisH != 0)
        {
            //左右入力時
            
            preAxisH = axisH;
            preAxisV = 0.0f;//左右の入力があったことを保存            
        }
        if (axisV != 0)
        {
            //上下入力時
            preAxisH = 0.0f;
            preAxisV = axisV;//上下の入力があったことを保存
        }

    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            //動いていない状態なら
            //速度を更新
            rb2d.linearVelocity = new Vector2(speed*axisH, speed*axisV);
            isMoving = true;
        }
    }
    
    //上下左右の入力終了後の自動運転
    private IEnumerator Move(float x, float y)
    {
        //格子点までの距離を計測
        if (x > 0.5f)
        {
            Distance = Mathf.Ceil(transform.position.x)-transform.position.x;
        }
        else if (x < -0.5f)
        {
            Distance = transform.position.x - Mathf.Floor(transform.position.x);
        }
        else if (y > 0.5f)
        {
            Distance = Mathf.Ceil(transform.position.y)-transform.position.y;
        }
        else if (y < -0.5f)
        {
            Distance = transform.position.y - Mathf.Floor(transform.position.y);
        }
        //格子点にいたるまでの時間
        float time = Distance / speed;
        //格子点にいたるまで待つ
        yield return new WaitForSeconds(time);
        //格子点についたら座標を整数値にし、速度を0にし、動いていない状態にする
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        rb2d.linearVelocity = Vector2.zero;
        isMoving = false;
    }
}
    
    