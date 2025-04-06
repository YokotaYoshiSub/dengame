using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isScrollX = false;//x方向に強制スクロールさせるかどうか
    public float scrollX = 0.0f;//x方向移動速度
    public float maxScrollX = 999f;//x正方向スクロール上限
    public float minScrollX = -999f;//x負方向スクロール上限
    public bool isScrollY = false;//y方向に強制スクロールさせるかどうか
    public float scrollY = 0.0f;//y方向移動速度
    public float maxScrollY = 999f;//y正方向スクロール上限
    public float minScrollY = -999f;//y負方向スクロール上限
    //カメラの3次元座標
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;

    GameObject player;//プレイヤー

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        z = transform.position.z;//z座標は固定
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            z = transform.position.z;//z座標は固定

            //------------------------x座標について----------------------------
            if (isScrollX)
            {
                //カメラのy座標の移動範囲を制限する
                if (transform.position.x >= maxScrollX)
                {
                    x = maxScrollX;
                }
                else if (transform.position.x <= minScrollX)
                {
                    x = minScrollX;
                }
                else
                {
                    //x方向強制スクロール
                    x = transform.position.x + scrollX * Time.deltaTime;
                }
            }
            else
            {
                //カメラのx座標の移動範囲を制限する
                if (player.transform.position.x >= maxScrollX)
                {
                    x = maxScrollX;
                }
                else if (player.transform.position.x <= minScrollX)
                {
                    x = minScrollX;
                }
                else
                {
                    //プレイヤーの位置に合わせる
                    x = player.transform.position.x;
                }
            }

            //------------------------------y座標について-----------------------------
            if (isScrollY)
            {
                //カメラのy座標の移動範囲を制限する
                if (transform.position.y >= maxScrollY)
                {
                    y = maxScrollY;
                }
                else if (transform.position.y <= minScrollY)
                {
                    y = minScrollY;
                }
                else
                {
                    //y方向強制スクロール
                    y = transform.position.y + scrollY * Time.deltaTime;
                }
            }
            else
            {
                //カメラのy座標の移動範囲を制限する
                if (player.transform.position.y >= maxScrollY)
                {
                    y = maxScrollY;
                }
                else if (player.transform.position.y <= minScrollY)
                {
                    y = minScrollY;
                }
                else
                {
                    //プレイヤーの位置に合わせる
                    y = player.transform.position.y;
                }
            }

            //カメラの座標を更新
            transform.position = new Vector3(x, y, z);
        }
    }
}
