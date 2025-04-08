using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class TextBreakEventTrigger : MonoBehaviour
{
    private TMP_Text textComponent;
    private Camera targetCamera;
    private void Awake()
    {
        this.textComponent = GetComponent<TMP_Text>();

        //カメラを取得する
        var rootCanvas = this.GetComponentInParent<Canvas>();
        if (rootCanvas == null)
        {
            switch (rootCanvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    this.targetCamera = null; break;
                case RenderMode.ScreenSpaceCamera:
                    this.targetCamera = rootCanvas.worldCamera; break;
                case RenderMode.WorldSpace:
                    this.targetCamera = rootCanvas.worldCamera; break;
            }
        }
        else
        {
            this.targetCamera = Camera.main;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //クリック座標を取得
        Vector3 touchPosition = Input.mousePosition;//マウス座標
        bool touchDown = Input.GetMouseButtonDown(0);

        if (0 < Input.touchCount)
        {
            //タップされている指の数が0より大きければ
            //シングルタップを前提としている
            Touch touchInfo = Input.GetTouch(0);
            touchPosition = touchInfo.position;//タップ座標
            touchDown = touchInfo.phase == TouchPhase.Began;
        }

        //クリック判定
        if (touchDown)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(
                this.textComponent, touchPosition, this.targetCamera);

            if (0 <= linkIndex)
            {
                TMP_LinkInfo linkInfo = this.textComponent.textInfo.linkInfo[linkIndex];

                Debug.Log($"Link Index: {linkIndex} with ID [{linkInfo.GetLinkID()}]" +
                    $"and Text \"{linkInfo.GetLinkText()}\" has been selected.");
            }
        }
        //アニメーションを再生
    }
}
