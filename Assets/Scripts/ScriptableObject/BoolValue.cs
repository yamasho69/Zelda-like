using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part42で作成。
[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver {
    public bool initialValue;

    //https://www.urablog.xyz/entry/2017/08/06/104652
    [HideInInspector]
    public bool RuntimeValue;
    public void OnAfterDeserialize() {
        RuntimeValue = initialValue;
    }//デシリアライズ後に読み込む。
    public void OnBeforeSerialize() { }//シリアライズ前に読み込む。

}
