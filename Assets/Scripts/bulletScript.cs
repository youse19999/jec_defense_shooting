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

    //状態
    public bool isCharged = false;

    //その他
    private float timeCount;
    private Vector3 direction;
    public string enemyTagName = "Enemy";
    Rigidbody rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

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
        if(collision.tag == enemyTagName)
        {

            if(isCharged)
            {
                
            }
            else
            {
                Destroy(this.gameObject);
            }
                
        }
    }
}
