using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

//Part16で追加。
public class Log : Enemy
{
    public Rigidbody2D myRigidbody2D; //Part34でpublicに変更。
    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius;//追跡半径
    public float attackRadius;//攻撃半径 大きいと主人公がダメージを受けない
    public Transform homePosition;//ホームポジション

    [Header("Animator")]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;//Part19で追加。
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;//targetをPlayerタグが付いているオブジェクトにする。
        anim.SetBool("wakeup", true);//Part34で追加。
    }

    // Update is called once per frame
    void FixedUpdate()　//Part19でFixeUpdateに変更。
    {
        CheckDistance();
    }

    //PlayerがLogの追跡半径内、攻撃半径外の時、LogはPlayerを追いかける。
    public virtual void CheckDistance() {//Part34でpublic virtualに変更。
        if(Vector3.Distance(target.position,transform.position)<= chaseRadius
            && Vector3.Distance(target.position,transform.position)>attackRadius) {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk 
                && currentState != EnemyState.stagger) {//Part19でこのif条件を追加。
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody2D.MovePosition(temp);
                               
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeup", true); //Part21で追加。
            }
            //Part21で追加。
        }else if(Vector3.Distance(target.position, transform.position) > chaseRadius) {
            anim.SetBool("wakeup", false);//Part21で追加。
        }
    }

    //Part21で追加
    private void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    //Part21で追加 Mathfクラス https://www.sejuku.net/blog/57063
    public void changeAnim(Vector2 direction) { //Part34でPublicに変更。
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {//x軸方向の絶対値が大きい場合
            if(direction.x > 0) { //x軸方向の力が0を超過
                SetAnimFloat(Vector2.right);
            }else if(direction.x < 0) {//x軸の方向の力が0未満
                SetAnimFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {//y軸方向の絶対値が大きい場合
            if (direction.y > 0) { //y軸方向の力が0を超過
                SetAnimFloat(Vector2.up);
            } else if (direction.y < 0) {//y軸の方向の力が0未満
                SetAnimFloat(Vector2.down);
            }
        }
    }

    //Part19で追加。
    public void ChangeState(EnemyState newState) {
        if(currentState != newState) {
            currentState = newState;
        }
    }
}
