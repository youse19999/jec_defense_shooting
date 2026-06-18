using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 目的：プレイヤーの処理。弾の生成、発射など
 * 作成者　伊藤
 * 最終編集者　伊藤
 * 
 * 編集履歴：
 * 2026/06/11　伊藤
 */
public class PlayerScript : MonoBehaviour
{
    //プレイヤーステータス
    float maxHP = 100;
    [SerializeField]
    float currentHP;
    float maxGauge;
    float currentGauge;
    //チャージ
    float currentChargePoint = 0.0f;
    float addpoint = 10.0f;
    public float chargePoint = 20.0f;
    //状態
    public bool isDead = false;
    bool isCharge = false;
    //その他
    Camera mainCamera;
    public GameObject bullet;
    static PlayerScript instance;
    public static PlayerScript GetInstance()
    {
        return instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        currentHP = 100;
        if(instance == null)
        {
            instance = this;
        }
        else
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            SceneManager.LoadScene("Result");
            return; 
        }
        //弾発射の処理
        Shot();
    }

    void Shot()
    {

        //通常攻撃
        if (Input.GetMouseButtonUp(0))
        {
            //クリック位置から発射角度を計算し生成
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
          
            Vector2 playerPosition = this.transform.position;
            
            Vector2 direction = mousePosition - playerPosition;
            direction.Normalize();

            float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
            GameObject bulletObject = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, angle));
            float speed = bulletObject.GetComponent<bulletScript>().bulletSpeed;
            Rigidbody2D rigidbody2D = bulletObject.GetComponent<Rigidbody2D>();
            rigidbody2D.linearVelocity = direction * speed;
            currentChargePoint = 0;
        }

        //チャージ
        if (Input.GetMouseButton(0))
        {
            //チャージ完了処理
            if (currentChargePoint >= chargePoint)
            {
                isCharge = true;
                Debug.Log("チャージ完了");
            }
            else
            {
                isCharge = false;
            }

            if (isCharge) { return; }
            currentChargePoint +=  Time.deltaTime* addpoint;
 
        }
    }

    //受け渡し関数
    public void Damage()
    {
        //ダメージを受ける
        currentHP -= 20;
        //HP０で死亡処理
        if (currentHP <= 0) { 
            isDead = true; 
        }
    }


    public bool GetIsCharged()
    {
        return isCharge;
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}
