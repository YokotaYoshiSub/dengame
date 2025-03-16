using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //上下左右のスクロールリミット
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public GameObject subScreen; //サブスクリーン

    public bool isForceScrollX = false;//X方向強制スクロールフラグ
    public float forceScrollSpeedX = 0.5f;//X方向強制スクロールの速度
    public bool isForceScrollY = false;//Y方向強制スクロールフラグ
    public float forceScrollSpeedY = 0.5f;//Y方向強制スクロールの速度 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = 
            GameObject.FindGameObjectWithTag("Player");//タグからプレイヤーを探す
        if (player != null)
        {
            //カメラの更新座標
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;
            //横同期させる
            //両端に移動制限をつける
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }
            //縦同期させる
            if (isForceScrollX)
            {
                //横強制スクロール
                x = transform.position.x + (forceScrollSpeedX*Time.deltaTime);
            }
            //上下に移動制限をつける
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            //縦同期させる
            if (isForceScrollY)
            {
                y = transform.position.y + (forceScrollSpeedY*Time.deltaTime);
            }
            //カメラ位置のVector3を作る
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;
            //サブスクリーンスクロール
            if (subScreen != null)
            {
                //y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x/2.0f, y/2.0f, z);
                subScreen.transform.position = v;
            }
        }
    }
}
