using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.GetComponent<Text>().text = GameManager.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
