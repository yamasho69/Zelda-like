using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Part36で作成。
public enum DoorType {
    Key,
    enemy,
    button
}

public class Door : Interactable{

    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public GameObject triggerArea;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {//スペースキーを押したとき
            if (playerInRange && thisDoorType == DoorType.Key) {//ドアがkeyタイプでプレイヤーが範囲にいるとき
                if(playerInventory.numberOfKeys > 0) {//鍵を一つ以上持っているとき
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open() {//スプライトレンダラーとボックスコライダーを消す。
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        triggerArea.SetActive(false);//トリガーエリアを無効にする。動画中にはない。
    }

    public void Close() {

    }
}
