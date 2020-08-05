using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Part31で作成。
public class Interactable : MonoBehaviour{

    public bool playerInRange;
    public Signal context;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    //PlayerがCollider2Dに触れたときに作動。
    //引数をCollisionからotherに変えないと、Player以外のオブジェクトが触れたときにも作動してしまう。
    virtual public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerInRange = true;
            //contextOn.Raise();//Part30で追加。
            context.Raise();
        }
    }

    //PlayerがCollider2Dから離れたときに作動
    virtual public  void OnTriggerExit2D(Collider2D other) {//Part33が上手く動作しなかったため、virtualを追加。
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerInRange = false;
            //contextOff.Raise();//Part30で追加。
            context.Raise();
        }
    }
}
