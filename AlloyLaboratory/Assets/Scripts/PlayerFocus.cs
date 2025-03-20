using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    float axisH = 0.0f;
    float axisV = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (axisV == 0)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }
        if (axisH == 0)
        {
            axisV = Input.GetAxisRaw("Vertical");
        }
        
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(axisH, axisV); 
        //Debug.Log(transform.position);
    }
}
