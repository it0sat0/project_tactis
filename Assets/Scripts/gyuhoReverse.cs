using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyuhoReverse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
