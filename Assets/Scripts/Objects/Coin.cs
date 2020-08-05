using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part39で作成。
public class Coin : Powerup{

    public Inventory playerInventory;
    
    // Start is called before the first frame update
    void Start(){
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerInventory.coins++;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
