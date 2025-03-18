using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;//X移動距離
    public float moveY = 0.0f;//Y移動距離
    public float times =0.0f;//時間
    public float wait = 0.0f;//停止時間
    public bool isMoveWhenOn = false;//乗った時に動くフラグ
    public bool isCanMove = true;//動くフラグ
    Vector3 startPos;//初期位置
    Vector3 endPos;//移動先の位置
    bool isReverse = false;//反転フラグ

    float movep = 0;//0~1でどれくらい移動したかを示す

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;//初期位置
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);//移動先の位置
        if(isMoveWhenOn)
        {
            //乗った時に動くので最初は動かさない
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            float distance = Vector2.Distance(startPos, endPos);//移動距離
            float ds = distance / times;//1秒の移動距離
            float df = ds * Time.deltaTime;//1fの移動距離
            movep += df/distance;//0~1でどれくらい移動したかを示す
            //Debug.Log(movep);
            if (isReverse)
            {
                transform.position = Vector2.Lerp(endPos,startPos,movep);//逆移動
            }
            else
            {
                transform.position = Vector2.Lerp(startPos,endPos,movep);//逆移動
            }
            if (movep >= 1.0f)
            {
                movep = 0.0f;//移動補完値リセット
                isReverse = !isReverse;//移動方向を逆転
                isCanMove = false;//移動停止
                if (isMoveWhenOn == false)
                {
                    //乗った時に動くフラグoff
                    Invoke("Move", wait);//wait秒後に逆方向に移動し始める
                }
            }
        }
    }

    //移動フラグを立てる
    public void Move()
    {
        isCanMove = true;
    }

    //移動フラグをおろす
    public void Stop()
    {
        isCanMove =false;
    }

    //接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //接触したのがプレイヤーなら移動床の子にする
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //乗った時に動くフラグＯＮなら
                isCanMove = true; //移動フラグを立てる
            }
        }
    }
    //接触終了
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Player")
        {
            //接触したのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else 
        {
            fromPos = startPos;
        }
        //移動線
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
