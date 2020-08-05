using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part38で作成。
public class Heart : Powerup{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
                //playerHealth.RuntimeValue += amountToIncrease; 動画でここにあった。ここにあると最大値以上に体力回復できてしまう。
            if (playerHealth.initialValue > heartContainers.RuntimeValue * 2f) {//現体力が最大値未満の場合
                playerHealth.RuntimeValue += amountToIncrease;
                playerHealth.initialValue = heartContainers.RuntimeValue * 2f;
                powerupSignal.Raise();
            }
            Destroy(this.gameObject);
        }
    }
}
