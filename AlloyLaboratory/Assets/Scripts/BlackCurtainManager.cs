using UnityEngine;
using UnityEngine.UI;

public class BlackCurtainManager : MonoBehaviour
{
    public bool isActiveOnStart;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        if (!isActiveOnStart)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveOnStart)
        {
            FadeOut();
        }
    }

    void FadeOut()
    {
        Debug.Log(image.color);
        image.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
    }
}
