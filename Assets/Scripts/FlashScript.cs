using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour
{
    public float speed = 1.0f;
    public Text text;
    private float time;


    void Start()
    {

    }

    void Update()
    {
            text.color = GetAlphaColor(text.color);
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
