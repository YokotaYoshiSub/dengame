using UnityEngine;

public class EventProtector : MonoBehaviour
{
    public string awayDirection = "up";
    public string person;
    [Multiline(2)]
    public string text;
    public int eventProgress;//GameManager.eventProgressがこの値より大きければ＝必要なイベントをこなしたらオブジェクトを破壊

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.eventProgress >= eventProgress)
        {
            Destroy(gameObject);
        }
    }
}
