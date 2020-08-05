using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part34で作成。
public class PatrolLog : Log{

    public Transform[] path;//パトロールポイントを入れる配列
    public int currentPoint;//次のパトロールポイントの配列番号
    public Transform currentGoal;//次のパトロールポイント
    public float roundingDistance;

    public override void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius//PlayerがLogの追跡半径内、攻撃半径外の時、LogはPlayerを追いかける。
            && Vector3.Distance(target.position, transform.position) > attackRadius) {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk//Enemyがidle状態またはwalk状態でstagger状態でないとき
                && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody2D.MovePosition(temp);
                anim.SetBool("wakeup", true); 
            }
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {//Playerが追跡半径外のとき
            if (Vector3.Distance(transform.position, path[currentPoint].position)>=roundingDistance) {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody2D.MovePosition(temp);
            } else {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal() {
        if(currentPoint == path.Length - 1) {//最後のポイントに行ったら最初のポイントを目標に変更。
            currentPoint = 0;
            currentGoal = path[0];
        } else {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
