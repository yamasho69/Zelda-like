using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Part43で作成。
public class AreaEnemy : Log{
    public Collider2D boundary;//敵が追跡してくるエリア(boxColleder2Dで範囲指定)

    public override void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && boundary.bounds.Contains(target.transform.position)) {//Part43でこの1行を作成。target(プレイヤー)が追跡エリア内にいること。
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody2D.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("wakeup", true); 
            }
            //Part21で追加。
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius
            || !boundary.bounds.Contains(target.transform.position)) {//Part43でこの1行を作成。
            anim.SetBool("wakeup", false);
        }
    }
}
