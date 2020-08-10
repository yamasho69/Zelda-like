using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SuperTiled2Unity.Editor;

//Part46で作成。
public class Room : MonoBehaviour{
    public Enemy[] enemies;
    public Pot[] pots;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            //Activate all enemies and pots
            for(int i = 0;i<enemies.Length; i++) {
                ChangeActivation(enemies[i],true);
            }
            for (int i = 0; i < pots.Length; i++) {
                ChangeActivation(pots[i], true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            //Deactivate all enemies and pots
            for (int i = 0; i < enemies.Length; i++) {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++) {
                ChangeActivation(pots[i], false);
            }
        }
    }

    void ChangeActivation(Component component,bool activation) {
        component.gameObject.SetActive(activation);
    }
}
