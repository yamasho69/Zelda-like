using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part33で作成。
[CreateAssetMenu]
public class Inventory : ScriptableObject{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;//Part39で追加。

    public void AddItem(Item itemToAdd) {
        //Is the item a key?
        if (itemToAdd.isKey) {
            numberOfKeys++;
        } else {
            if (!items.Contains(itemToAdd)) {
                items.Add(itemToAdd);
            }
        }
    }
}
