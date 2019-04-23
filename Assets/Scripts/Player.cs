using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    //public float jumpHeight = 4;
    //public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    int weaponNum = 0;

    public int PlayerHP;
    public Text PlayerText;

    Vector2 playerT;
    public GameObject generator;

    public Shot m_shotPrefab; // 弾のプレハブ
    public float m_shotSpeed; // 弾の移動の速さ
    public float m_shotAngleRange; // 複数の弾を発射する時の角度
    public float m_shotTimer; // 弾の発射タイミングを管理するタイマー
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）

    //地雷関係
    public GameObject LM; //地雷のプレハブ
    public Rigidbody2D LM_r;

    //レーザー関係
    public GameObject laser;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    float x = Input.GetAxisRaw("Horizontal");

    Controller2D controller;

    void Start()
    {
        playerT = GameObject.Find("player").transform.position;
        controller = GetComponent<Controller2D>();
        /*
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
        */

        //地雷関係
        LM_r = GetComponent<Rigidbody2D>();

        PlayerText.text = "tactisHP : " + PlayerHP.ToString();

    }

    void Update()
    {
        if (PlayerHP <= 0)
        {
            SceneManager.LoadScene("Gameover");
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {   
            if (weaponNum == 3)
            {
                weaponNum = 0;
            }
            else
            {
                weaponNum++;
            }
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        Vector2 scale = transform.localScale;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 右方向に移動中
            scale.x = -1; // 反転する（左向き）
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 左方向に移動中
            scale.x = 1; // そのまま（右向き）
        }
        // 代入し直す
        transform.localScale = scale;


        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        // プレイヤーのスクリーン座標を計算する
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから見たマウスカーソルの方向を計算する
        var direction = Input.mousePosition - screenPos;

        // マウスカーソルが存在する方向の角度を取得する
        var angle = Utils.GetAngle(Vector3.zero, direction);

        // プレイヤーがマウスカーソルの方向を見るようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;


        if (weaponNum == 0)
        { //弾丸
            /*
            // 弾の発射タイミングを管理するタイマーを更新する
            m_shotTimer += Time.deltaTime;

            // まだ弾の発射タイミングではない場合は、ここで処理を終える
            if (m_shotTimer < m_shotInterval) return;

            // 弾の発射タイミングを管理するタイマーをリセットする
            m_shotTimer = 0;
            */
            if (Input.GetMouseButtonDown(0)) {
                ShootNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);
            }
        }
        else if (weaponNum == 1)
        { //レーザー
                generator.GetComponent<WGenerator>().LaserInstantiate();
                Debug.Log("laser");
        }
        else if (weaponNum == 2)
        { //ミサイル
            if (Input.GetMouseButtonDown(0)) {
                generator.GetComponent<WGenerator>().MisileInstantiate();
                Debug.Log("misile");
            }
        }
        else if (weaponNum == 3)
        { //地雷
            if (Input.GetMouseButtonDown(0))
            {
                generator.GetComponent<WGenerator>().LMinstantiate();
                Debug.Log("landmine");
            }
        }
    }
    //private void OnMouseDrag(){
    // 弾を発射する
    //ShootNWay( angle, m_shotAngleRange, m_shotSpeed, m_shotCount );
    //}

    // 弾を発射する関数
    private void ShootNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        // 弾を複数発射する場合
        if (1 < count)
        {
            // 発射する回数分ループする
            for (int i = 0; i < count; ++i)
            {
                // 弾の発射角度を計算する
                var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);

                // 発射する弾を生成する
                var shot = Instantiate(m_shotPrefab, pos, rot);

                // 弾を発射する方向と速さを設定する
                shot.Init(angle, speed);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            var shot = Instantiate(m_shotPrefab, pos, rot);

            // 弾を発射する方向と速さを設定する
            shot.Init(angleBase, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerHP--;
        }

        PlayerText.text = "tactisHP : " + PlayerHP.ToString();
    }
}
