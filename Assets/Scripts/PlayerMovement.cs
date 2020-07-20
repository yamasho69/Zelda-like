﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part11で追加。現在のプレイヤーの状態を判別
public enum PlayerState {
    walk,
    attack,
    interact,
    stagger, //Part20で追加
    idle//Part20で追加
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;//Part11で追加。主人公の状態。walk,attak,interact
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;//Part23で追加。
    public Signal playerHealthSignal;//Part24で追加。

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX",  0); //Part14で追加。
        animator.SetFloat("moveY", -1); //同上
    }

    // Update is called once per frame
    //プレイヤーの移動はFixedUpdateでないとスムーズにならない模様。
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
            UpdateAnimationAndMove();
        }
    }

    //Part11で追加
    //GetButtonDownはFixedUpdateだと反応が悪い。
    //http://mediamonster.blog.fc2.com/blog-entry-23.html
    private void Update() {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());//コルーチン。https://www.sejuku.net/blog/83712
        }
    }

    private IEnumerator AttackCo() {
        animator.SetBool("attacking", true);
        yield return null;//ワンフレーム停止
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(1f);//1f待つ
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove() {
        if (change != Vector3.zero && currentState != PlayerState.attack) {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter() {
        change.Normalize(); //Part14で追加。ベクトルの正規化。斜めでも１になる。
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime,float damage) {//damageでPart24で追加。
        currentHealth.RuntimeValue -= damage;//currentHealthからdamageを引く。Part25でRuntimeValueに変更。
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0) {//Part24で追加。　https://create.unity3d.com/jp-protips-architect-with-scriptable-objects?elqTrackId=110800b3268644bc9340264f8cc7f818&elq=00000000000000000000000000000000&elqaid=2115&elqat=2&elqCampaignId=
            StartCoroutine(KnockCo(knockTime));//damageを引かれても、currentHealthが1以上なら生きているのでノックバックする。
        } else {
            this.gameObject.SetActive(false);//Part25で追加。ライフゼロでプレイヤー消える。
        }
    }

    //Part20で追加
    private IEnumerator KnockCo(float knockTime) {
        if (myRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;//Part20で追加。
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
