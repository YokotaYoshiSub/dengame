using UnityEngine;

public class EventProtector : MonoBehaviour
{
    public string awayDirection = "up";
    public string person;
    [Multiline(2)]
    public string text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.eventPoint == 1)
        {
            Destroy(gameObject);
        }
    }
}
