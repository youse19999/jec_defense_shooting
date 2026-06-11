using Unity.Hierarchy;
using UnityEngine;

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
    Camera mainCamera;
    public GameObject bullet;

    //プレイヤーステータス
    float maxHP = 100;
    float maxGauge;
    float currentGauge;
    float currentHP;
    //チャージ
    float currentPoint = 0;
    float addpoint = 10.0f;
    float chargePoint = 50.0f;
    //状態
    bool isDead = false;
    bool isCharge = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        
 
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        //弾発射の処理
        Shot();

        //ダメージ判定
        Damage();
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

            //チャージしていた場合溜めをリセット
            isCharge = false;
            currentPoint = 0;
        }

        //チャージ処理
        if (Input.GetMouseButton(0))
        {
            if (isCharge) { return; }
                currentPoint +=  Time.deltaTime* addpoint;
            Debug.Log(currentPoint);
            //チャージ完了処理
            if(currentGauge >= chargePoint) { isCharge = true; }
            
        }
    }

    void Damage()
    {
        //ダメージを受ける

        //HP０で死亡処理
        //if(currentHP <= 0) { isDead = true; }
    }
}
