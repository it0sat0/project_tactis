using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LM;
    public GameObject Misile;
    public GameObject laser;
    GameObject Laser;
    Rigidbody2D LaserR;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LMinstantiate()
    {
        Instantiate(LM, transform.position, Quaternion.identity);
    }

    public void MisileInstantiate()
    {
        Instantiate(Misile, transform.position, Quaternion.identity);
    }

    public void LaserInstantiate()
    {

        if (Input.GetMouseButtonDown(0))
        {

            // 弾（ゲームオブジェクト）の生成
            GameObject clone = Instantiate(laser, transform.position, Quaternion.identity);

            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

            // 弾に速度を与える
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * speed;

        }
    }
}
