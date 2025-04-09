using UnityEngine;

public class EventItemController : MonoBehaviour
{
    public string person;
    [Multiline(2)]
    public string text;
    public string itemName;
    [Multiline(2)]
    public string itemData;
    bool available = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (available)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerFocus")
        {
            available = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerFocus")
        {
            available = false;
        }
    }
}
