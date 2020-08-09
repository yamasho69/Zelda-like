using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part45で作成。
public class TurretEnemy : Log{

    public GameObject projectile;
    public float fireDelay;//弾の連射間隔
    private float fireDelaySeconds;
    public bool canFire;

    //弾の連射間隔を調整する
    private void Update() {
        fireDelaySeconds -= Time.deltaTime;
        if(fireDelaySeconds <= 0) {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    //PlayerがLogの追跡半径内、攻撃半径外の時、LogはPlayerを追いかける。
    public override void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius) {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger) {

                /*Part45で無効(Logでは有効)
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody2D.MovePosition(temp);*/

                //Part45で追加(開始)。
                if (canFire) {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    //Part45で追加(ここまで)。

                    canFire = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("wakeup", true);
                }
            }
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
            anim.SetBool("wakeup", false);
        }
    }
}
