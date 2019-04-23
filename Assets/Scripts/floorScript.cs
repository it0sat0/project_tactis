using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorScript : MonoBehaviour
{
    public GameObject LMM;
    public GameObject Bomb;

    // Start is called before the first frame update
    void Start()
    {
        LMM.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LandMine")
        {
            LMM.SetActive(true);
        }
    }

}
