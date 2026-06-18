using UnityEngine;

/*
 * 目的：くまの行動処理
 * 作成者　小林
 * 最終編集者　小林
 * 
 * 編集履歴：
 * 2026/06/11 小林作成
 */

public class bearScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int MoveSpeed=0;
    private int time = 0;
    private Animator anim = null;
    public Transform targetPosition;
  

    //ステートでの動きの管理
    public enum State
    {
        Walk,
        Wait,
        Attack,
        Dead
    }
    State state;

    void BearWalk()
    {

        transform.position = Vector3.MoveTowards(transform.position,
                         targetPosition.position, MoveSpeed * Time.deltaTime);

        float distance=Vector3.Distance(transform.position, targetPosition.position);
        if(distance < 0.1f)
        {
            state = State.Attack;
            anim.SetBool("walk", false);
            time = 0;
        }
    }

    void BearAttack()
    {
        if (time >= 120)
        {
            state = State.Wait;
            anim.SetBool("Attack", false);
            time = 0;
        }
    }

    void BearWait()
    {
        if(time >= 120)
        {
            state = State.Attack;
            anim.SetBool("Attack", true);
            time = 0;
        }
    }

    void BearDead()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        state = State.Walk;
        anim=GetComponent<Animator>();
        anim.SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        time++;

        switch (state)
        {
            case State.Walk:    BearWalk();    break;
            case State.Attack:  BearAttack();  break;
            case State.Wait:    BearWait();    break;
            case State.Dead:    BearDead();    break;
        }
    }
}
