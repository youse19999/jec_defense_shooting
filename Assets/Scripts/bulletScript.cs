using UnityEngine;

/*
 * 目的：弾のステータスなどの管理。攻撃力、スピード、寿命など
 * 作成者　伊藤
 * 最終編集者　伊藤
 * 
 * 編集履歴：
 * 2026/06/11　伊藤
 */
public class bulletScript : MonoBehaviour
{
    //弾ステータス
    public float bulletSpeed = 1.0f;　//速度
    public float bulletattackPoint = 1.0f; //攻撃力
    [SerializeField] private float bulletlifeTime = 5.0f;//寿命

    //その他
    public string enemyTagName = "Enemy";
    private float timeCount;
    private Vector3 direction;
    Rigidbody rigidBody;
    PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GameObject player = GameObject.Find("player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount >= bulletlifeTime) { Destroy(this.gameObject); }

    }

    //エネミーに衝突したら自壊
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(enemyTagName))
        {
            //チャージ弾
            if(playerScript.GetIsCharged())
            {
                Debug.Log("反応してます");
               //今いる敵を全消す
            }
            //通常弾
            else
            {
                //敵にダメージを与える処理。ダメージはbulletattackPoint(float)変数宣言済

                //Destroy(this.gameObject);
            }
                
        }
    }
}
