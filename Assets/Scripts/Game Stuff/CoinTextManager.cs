using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

//Part39で作成。
public class CoinTextManager : MonoBehaviour{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinCount() {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
