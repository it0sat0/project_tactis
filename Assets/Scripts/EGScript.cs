using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGScript : MonoBehaviour
{

    public GameObject Enemy;
    public float GenerateTime;
    public int MaxEnemy;
    public float ranX;

    float ranTime;
    float Timetime = 0;
    int EnemyC = 0;


    // Start is called before the first frame update
    void Start()
    {
        Timer();
    }

    // Update is called once per frame
    void Update()
    {

        Timetime += Time.deltaTime;

        if (ranTime < Timetime)
        {
            Instantiate(Enemy, new Vector2(Random.Range(-ranX, ranX),7f), Quaternion.identity);
            Timer();
            Timetime = 0;
            EnemyC++;
        }

        if (MaxEnemy <= EnemyC)
        {
            Destroy(this.gameObject);
        }
    }

    void Timer()
    {
        ranTime = Random.Range(0.0f, GenerateTime);
    }
}
