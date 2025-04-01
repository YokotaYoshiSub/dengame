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
    public bool onEvent = false;//イベント状態かどうか
    bool isRight = false;//右方向コルーチン開始フラグ
    bool isLeft = false;//左方向コルーチン開始フラグ
    bool isUp = false;//上方向コルーチン開始フラグ
    bool isDown = false;//下方向コルーチン開始フラグ
    //-------------------------HP関連------------------------
    public int maxHp = 3;//最大hp
    public static int hp = 3;//hp
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();//Rigidbody2Dの取得
        enemy = GameObject.FindGameObjectWithTag("Damage1");
        hp = maxHp;
    }

    // Update is called once per frame

    void Update()
    {
        //Debug.Log(new Vector2(axisH, axisV));
        
        //左シフトでダッシュ状態
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8.0f;
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

        //入力が1,-1から変更されたらコルーチン開始フラグオン
        if (preAxisH != 0 && preAxisH != axisH)
        {
            if (isCoroutineWorking)
            {
                //すでにコルーチンが働いていたら、入力の保存だけして抜ける
                preAxisH = axisH;
                preAxisV = axisV;
                return;
            }
            else if(preAxisH > 0.5f)
            {
                isRight = true;
            }
            else if(preAxisH < -0.5f)
            {
                isLeft = true;
            }
            preAxisH = axisH;
            preAxisV = axisV;
        }
        else if (preAxisV != 0 && preAxisV != axisV)
        {
            //Debug.Log(preAxisV);
            if (isCoroutineWorking)
            {
                //すでにコルーチンが働いていたら、入力の保存だけして抜ける
                preAxisH = axisH;
                preAxisV = axisV;
                return;
            }
            else if(preAxisV > 0.5f)
            {
                isUp = true;
            }
            else if(preAxisV < -0.5f)
            {
                isDown = true;
            }
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
        if (onEvent)
        {
            //イベント中は移動しない
            return;
        }

        if (isRight)
        {
            //右移動フラグオンなら
            StartCoroutine(Move(1.0f, 0.0f));
        }
        else if (isLeft)
        {
            //左移動フラグオンなら
            StartCoroutine(Move(-1.0f, 0.0f));
        }
        else if (isUp)
        {
            //左移動フラグオンなら
            StartCoroutine(Move(0.0f, 1.0f));
        }
        else if (isDown)
        {
            //左移動フラグオンなら
            StartCoroutine(Move(0.0f, -1.0f));
        }

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
            else
            {
                transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            }
        }
        else if(!isCoroutineWorking)
        {
            //動いている状態かつコルーチン未始動なら
            //速さのみ更新→ダッシュに対応
            rb2d.linearVelocity = new Vector2(speed*axisH, speed*axisV);
        }
    }
    
    //----------------------上下左右の入力終了後の自動運転-------------------------
    private IEnumerator Move(float x, float y)
    {
        isCoroutineWorking = true;//コルーチン始動フラグ

        //すでに移動開始したので、コルーチンが重複しないよう方向フラグoff
        isRight = false;
        isLeft = false;
        isUp = false;
        isDown = false;

        float distance = 1.0f;//プレイヤーの現在位置から格子点までの距離
        float isGoal = 0.1f;//ゴールまでの距離がこれ以下だったらゴールとする


        while(true)
        {
            //ゴールまでの距離が一定以下なら以下の処理を毎フレーム行う
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

            //ゴールに近づいたらループを抜ける
            if (isGoal > distance)
            {
                break;
            }
            //distanceの値がおかしくなった時用
            //場所がほぼ格子点上ならそこで止める
            if (new Vector2(transform.position.x - Mathf.Round(transform.position.x), 
            transform.position.y - Mathf.Round(transform.position.y)).magnitude < 0.1f)
            {
                break;
            }

            //速度を更新
            rb2d.linearVelocity = new Vector2(speed * x, speed * y);
            yield return null;
        }

        //格子点についたら座標を整数値にし、速度を0にし、動いていない状態にする
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        rb2d.linearVelocity = Vector2.zero;
        isMoving = false;
        isCoroutineWorking = false;
    }

    //-----------------------------被弾モーション-------------------------
    private IEnumerator HitByEnemy()
    {
        //一瞬入力を受け付けない時間を設ける
        isMoving = true;
        isCoroutineWorking = true;

        float blownTime = 0;//吹っ飛ばされてからの時間
        float blownSpeed;//吹っ飛ばされる速さ

        //敵の方向に応じて吹っ飛ばす
        //吹っ飛ばす方向の正規ベクトル
        Vector2 blownDirection = new Vector2((transform.position.x - enemy.transform.position.x),(transform.position.y - enemy.transform.position.y)).normalized;
        //吹っ飛ばされる先
        Vector2 blownGoal = new Vector2(Mathf.Round(transform.position.x + blownDirection.x), Mathf.Round(transform.position.y + blownDirection.y));
        //吹っ飛ばされる強さ
        float blownForce = new Vector2(blownGoal.x - transform.position.x, blownGoal.y - transform.position.y).magnitude * 27f;

        //Debug.Log(blownForce);
        //Debug.Log(blownDirection);

        while(true)
        {
            //吹っ飛ばされているときの速さを更新
            blownSpeed = (0.3f - blownTime)*blownForce;
            rb2d.linearVelocity = new Vector2(blownDirection.x * blownSpeed, blownDirection.y * blownSpeed);
            blownTime += Time.deltaTime;

            if (blownTime > 0.3f)
            {
                //時間がたったら終了
                break;
            }

            yield return null;
        }

        //最終的にまた入力を受け付け、移動できるようにする
        isMoving = false;
        isCoroutineWorking = false;

        //このとき入力がなかったら格子点に移動する
        if (axisH == 0 && axisV == 0)
        {
            //実際には2~3fかけて移動させたい
            transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage1")
        {
            //敵に接触した時の処理
            hp -= 1;
            
            //敵と反対方向に弾き飛ばされる
            StartCoroutine(HitByEnemy());
        }
    }
}
    
    