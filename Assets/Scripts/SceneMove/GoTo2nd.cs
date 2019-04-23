using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo2nd : MonoBehaviour
{
    public EGScript EG;
    public EGScript12 EG1;
    public EGScript12 EG2;

    int EnemyNum = 0;
    int killedEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        EnemyNum += (EG.MaxEnemy + EG1.MaxEnemy + EG2.MaxEnemy);
        Debug.Log("EnemyNum=" + EnemyNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyNum <= killedEnemy)
        {
            Invoke("GoNext", 2);
        }
    }

    void GoNext()
    {
        SceneManager.LoadScene("Ending");
    }

    public void EnemyCountor()
    {
        killedEnemy++;
        Debug.Log("killedENemy="+killedEnemy);
    }
}
