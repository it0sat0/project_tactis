using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript2 : MonoBehaviour
{
    GameObject pine;
    GoTo2nd GT2;

    public float speed;
    public int EnemyHP;
    public GameObject Explosion;
    GameObject EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        pine = GameObject.FindGameObjectWithTag("pine");
        EventSystem = GameObject.Find("System");
        GT2 = EventSystem.GetComponent<GoTo2nd>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(0, -3.5f), step);

        if (EnemyHP <= 0)
        {
            GT2.EnemyCountor();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "LMS" || collision.gameObject.tag == "laser" || collision.gameObject.tag == "misile")
        {
            EnemyHP--;
        }


        if (collision.gameObject.tag == "pine")
        {
            GT2.EnemyCountor();
            Destroy(this.gameObject);
        }
    }
}
