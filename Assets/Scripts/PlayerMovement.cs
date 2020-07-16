﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part11で追加。現在のプレイヤーの状態を判別
public enum PlayerState {
    walk,
    attack,
    interact
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;//Part11で追加。主人公の状態。walk,attak,interact
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //プレイヤーの移動はFixedUpdateでないとスムーズにならない模様。
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
    }

    //Part11で追加
    //GetButtonDownはFixedUpdateだと反応が悪い。
    //http://mediamonster.blog.fc2.com/blog-entry-23.html
    private void Update() {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack) {
            StartCoroutine(AttackCo());//コルーチン
        }
    }

    private IEnumerator AttackCo() {
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(1f);//0.1f待つ
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
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime);
    }
}
