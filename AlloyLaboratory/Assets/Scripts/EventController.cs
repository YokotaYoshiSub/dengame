using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public int textNum = 6;//テキストの個数
    public string person0;
    public string text0;
    public string person1;
    public string text1;
    public string person2;
    public string text2;
    public string person3;
    public string text3;
    public string person4;
    public string text4;
    public string person5;
    public string text5;
    public string[] people;
    public string[] texts;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //texts配列、people配列を初期化
        texts = new string[textNum];
        people = new string[textNum];
        //texts配列にtextNumの個数分textを代入
        string[] maxTexts = {text0, text1, text2, text3, text4, text5};
        for (int i = 0; i < textNum; i++)
        {
            texts[i] = maxTexts[i];
        
        }
        //people配列にtextNumの個数分personを代入
        string[] maxPeople = {person0, person1, person2, person3, person4, person5};
        for (int i = 0; i < textNum; i++)
        {
            people[i] = maxPeople[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
