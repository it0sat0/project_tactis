using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public Rigidbody2D LM_r;

    

    // Start is called before the first frame update
    void Start()
    {
        Vector2 playerT = GameObject.Find("player").transform.position;

        Debug.Log("playerT = " + playerT.x);
        LM_r = GetComponent<Rigidbody2D>();

        if (playerT.x <= 0.0f)
        {
            LM_r.AddForce(new Vector2(-50, 50));
        }
        else if(playerT.x > 0.0f)
        {
            LM_r.AddForce(new Vector2(50, 50));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") {
            Destroy(this.gameObject, 0.05f);
        }
    }

}
