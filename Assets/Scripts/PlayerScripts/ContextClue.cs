using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Part30で作成。
public class ContextClue : MonoBehaviour{

    public GameObject contextClue;
    public bool contextActive = false;//Part30_1で追加

    public void ChangeContext() {
        contextActive = !contextActive;
        if (contextActive) {
            contextClue.SetActive(true);
        } else {
            contextClue.SetActive(false);
        }
    }

    /*public void Enable() {
        contextClue.SetActive(true);
    }

    public void Disable() {
        contextClue.SetActive(false);
    }*/

}
