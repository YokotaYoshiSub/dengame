using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
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
    public void ShowHint()
    {
        hint.SetActive(true);
        hintImage.SetActive(true);
        hintBackButton.SetActive(true);
        Time.timeScale = 0;
    }
}
