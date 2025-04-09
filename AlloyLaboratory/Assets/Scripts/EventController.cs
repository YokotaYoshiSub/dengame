using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public int textNum = 6;//テキストの個数
    [SerializeField]
    string person0;
    [SerializeField, Multiline(2)]
    string text0;
    [SerializeField]
    string person1;
    [SerializeField, Multiline(2)]
    string text1;
    [SerializeField]
    string person2;
    [SerializeField, Multiline(2)]
    string text2;
    [SerializeField]
    string person3;
    [SerializeField, Multiline(2)]
    string text3;
    [SerializeField]
    string person4;
    [SerializeField, Multiline(2)]
    string text4;
    [SerializeField]
    public string person5;
    [SerializeField, Multiline(2)]
    string text5;
    public string[] people;
    public string[] texts;
    public int eventPoint;//フラグ進行用のイベントポイント

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
