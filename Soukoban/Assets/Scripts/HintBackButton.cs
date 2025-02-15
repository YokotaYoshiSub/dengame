using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBackButton : MonoBehaviour
{
    public GameObject hint;
    public GameObject hintImage;
    public GameObject hintBackButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideHint()
    {
        hint.SetActive(false);
        hintImage.SetActive(false);
        hintBackButton.SetActive(false);
        Time.timeScale = 1;
    }
}
