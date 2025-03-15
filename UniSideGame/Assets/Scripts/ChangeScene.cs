using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン切り替えのため
public class ChangeScene : MonoBehaviour
{
    public string sceneName;//読み込むシーン名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
