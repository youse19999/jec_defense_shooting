using JetBrains.Annotations;
using UnityEngine;

/*
 * 目的：くまのスポーン
 * 作成者　小林
 * 最終編集者　小林
 * 
 * 編集履歴：
 * 2026/06/11 小林作成
 * 2026/06/18 小林
 */


public class bearGenerator : MonoBehaviour
{
    public GameObject bearPrefab;
    public int maxSpownTime=10;//ランダム生成の最大生成時間
    private float count;//時間管理
    private float RandomY;//ランダムで生成座標を決める 
    [SerializeField] Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count-=Time.deltaTime;

        if (count<=0)
        {
            RandomY = Random.Range(-3.5f, 3.5f);
            GameObject bearObj=Instantiate(bearPrefab,new Vector3(10,RandomY,0),Quaternion.identity);

            bearScript bear = bearObj.GetComponent<bearScript>();
            bear.targetPosition = target;

            count = Random.Range(1, maxSpownTime);
        }
    }
}
