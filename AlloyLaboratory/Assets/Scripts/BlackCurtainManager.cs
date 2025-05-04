using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BlackCurtainManager : MonoBehaviour
{
    //public bool isActiveOnStart;
    public float blackTime;//暗闇が完全に晴れるまでの時間
    bool isFadingOut;
    float fadeOutTime = 1f;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        /*
        if (!isActiveOnStart)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        blackTime -= Time.deltaTime;
        

        if (blackTime > 0f && blackTime < fadeOutTime)
        {
            //時間になったら1秒かけて透明に
            isFadingOut = true;
        }
        else if (blackTime >= -0.5f && blackTime <= 0f)
        {
            image.color = new Color(0f, 0f, 0f, 0f);//確実に完全に透明に
            isFadingOut = false;
        }

        if (isFadingOut)
        {
            FadeOut();
        }
    }

    void FadeOut()
    {
        //Debug.Log(image.color);
        if (image.color.a >= 0)
        {
            //1秒で透明になる
            image.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
        }
    }

    public IEnumerator FadeIn()
    {
        float time = 0f;
        //1秒で暗くなる
        while (true)
        {
            time += Time.deltaTime;
            image.color += new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return null;

            if (time >= 1f)
            {
                image.color = new Color(0f, 0f, 0f, 1f);
                yield break;
            }
        }
    }
}
