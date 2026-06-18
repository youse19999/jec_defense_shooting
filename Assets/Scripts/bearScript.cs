using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/*
 * 目的：くまの行動処理
 * 作成者　小林
 * 最終編集者　小林
 * 
 * 編集履歴：
 * 2026/06/11 小林作成
 * 2026/06/18 小林
 */

public class bearScript : MonoBehaviour
{
    private bool awaitableStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int moveSpeed=0;//移動速度
    public int hp = 10;//体力
    public int attackpoint = 20;//攻撃力
    public static int point =0;//倒された時のポイント
    private int time = 0;//時間管理系
    private Animator anim = null;
    public Transform targetPosition;
  

    //ステートでの動きの管理
    public enum State
    {
        Walk,       //歩き中
        Wait,       //待機中
        Attack,     //攻撃中
    }
    State state;

    void BearWalk()
    {
        //目的地へ移動
        transform.position = Vector3.MoveTowards(transform.position,
                         targetPosition.position, moveSpeed * Time.deltaTime);

        float distance=Vector3.Distance(transform.position, targetPosition.position);
        if(distance < 0.1f)
        {
            state = State.Attack;
            anim.SetBool("walk", false);
            time = 0;
        }
    }

    async Awaitable BearAttack()
    {
        if(awaitableStarted)
        {
            return;
        }
        awaitableStarted = true;
        while (true)
        {
            //攻撃が当たった処理

            PlayerScript.GetInstance().Damage();
            await Awaitable.WaitForSecondsAsync(1.0f);
            //Stateを待機にする
            if (time >= 120)
            {
                Debug.Log("Wait");
                state = State.Wait;
                anim.SetBool("Attack", false);
                time = 0;
            }
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

    public void BearDead()
    {
        if (hp <= 0)
        {
            point++;
            ResultScoreTest.score = point;
            Destroy(gameObject);//objectを消去
        }
    }

    void Start()
    {
        state = State.Walk;
        anim=GetComponent<Animator>();
        anim.SetBool("walk", true);
    }

    // Update is called once per frame
    async void Update()
    {
        time+=(int)Time.deltaTime;
        BearDead();
        switch (state)
        {
            case State.Walk: BearWalk(); break;
            case State.Attack: { await BearAttack(); } break;
            case State.Wait: BearWait(); break;
        }
    }
}
