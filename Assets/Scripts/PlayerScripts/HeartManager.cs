using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part24で作成。
public class HeartManager : MonoBehaviour{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue plyaerCurrentHealth; //Part25で追加。今のプレイヤーのライフチェック

    // Start is called before the first frame update
    void Start(){
        InitHearts();
    }

    public void InitHearts() {
        for(int i = 0; i < heartContainers.initialValue; i++) {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    //Part25で追加。
    public void UpdateHearts() {
        float tempHealth = plyaerCurrentHealth.RuntimeValue / 2; //Healthが5ならば、ハートは2．5個になるため
        for(int i = 0;i < heartContainers.initialValue; i++) {//ハート最大値分繰り返す。
            if(i <= tempHealth-1) {//tempHealth-1がi以上ならフルハートを入れる。
                //Full Heart
                hearts[i].sprite = fullHeart;
            }else if(i >= tempHealth) {//tempHealthがi以下なら空ハートを入れる。
                //empty Heart
                hearts[i].sprite = emptyHeart;
            } else {//上記以外つまり0．5が存在する場合は半ハートを入れる。
                //half full heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
