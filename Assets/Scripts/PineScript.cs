using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PineScript : MonoBehaviour
{
    public Text pineText;
    public int pineHP;

    // Start is called before the first frame update
    void Start()
    {
        pineText.text = "pineHP : " + pineHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (pineHP <= 0)
        {
            SceneManager.LoadScene("Gameover");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            pineHP--;
            pineText.text = "pineHP : " + pineHP.ToString();
        }
    }
}
