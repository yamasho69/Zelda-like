using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Part17で作成。
public class Knockback : MonoBehaviour
{
    public float thrust;//推力
    public float knockTime;
    public float damage;//Part22で追加。
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("breakable")&& this.gameObject.CompareTag("Player")){//Part20でPlayerHitから移動,&&以降はPart20で追加。追加しないと敵が触れてもツボが壊れる。
            other.GetComponent<Pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy")||other.gameObject.CompareTag("Player")){//Part20でPlayerを追加
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);//その質量を使用して、rigidbodyに瞬時に速度変化を追加　//http://nakamura001.hatenablog.com/entry/20120320/1332224186
                if (other.gameObject.CompareTag("enemy")&& other.isTrigger) {//&&以降はPart22で追加。
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;//Part19で追加。
                    other.GetComponent<Enemy>().Knock(hit, knockTime,damage);
                }
                //Part20で追加。
                if (other.gameObject.CompareTag("Player")) {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger) {//Part24でこの行を追加。stagger中はstaggerにならない。
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    } //damageはPart24で追加。
                }

                //enemy.isKinematic = false;//物理演算の影響を受けるようにする。//https://ekulabo.com/rigidbody-is-kinematic
                //StartCoroutine(KnockCo(hit)); Part20で無効化
            }
        }
    }

    /*Part20で無効化
    private IEnumerator KnockCo(Rigidbody2D enemy) {
        if(enemy != null) {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            //enemy.isKinematic = true;//物理演算の影響を受けないようにする。
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;//Part19で追加。
        }
    }*/
}
