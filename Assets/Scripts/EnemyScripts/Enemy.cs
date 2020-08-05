using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part19で追加。
public enum EnemyState {
    idle,
    walk,
    attack,
    stagger
}

//Part16で追加。敵を制御するスクリプト。
public class Enemy : MonoBehaviour
{ 
    [Header("State Machine")]//Part41で追加。
    public EnemyState currentState;//Part19で追加。
    public FloatValue maxHealth; //Part22で追加。
    public float health;

    [Header("Enemy Stats")]//Part41で追加。
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]//Part41で追加。
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;//Part41で追加。

    //Part22で追加。
    private void Awake() {
        health = maxHealth.initialValue;//maxHealthをhealthに代入
    }
    
    //Part22で追加。
    private void TakeDamage(float damage) {
        health -= damage;
        if(health <= 0) {
            DeathEffect();//Part37で追加。
            this.gameObject.SetActive(false);
        }
    }

    //Part37で追加。
    private void DeathEffect() {
        if(deathEffect != null) {
            //https://www.sejuku.net/blog/48180 Instantiate関数(コピー元オブジェクト、コピー先座標、向き)
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay); //Part41で1fからdeathEffectDelayに変更。
        }
    }

    public void Knock(Rigidbody2D myRigidobody,float knockTime,float damage) {
        StartCoroutine(KnockCo(myRigidobody, knockTime));
        TakeDamage(damage);
    }

    //Part20で追加。
    private IEnumerator KnockCo(Rigidbody2D myRigidbody,float knockTime) {
        if (myRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            //enemy.isKinematic = true;//物理演算の影響を受けないようにする。
            currentState = EnemyState.idle;//Part19で追加。
            myRigidbody.velocity = Vector2.zero;//Part20で追加。
        }
    }

}
