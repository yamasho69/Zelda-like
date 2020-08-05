using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part33で作成。
[CreateAssetMenu]
public class Item : ScriptableObject{
    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;
}
