using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part22で追加。
[CreateAssetMenu]
public class FloatValue : ScriptableObject,ISerializationCallbackReceiver
{
    public float initialValue;

    //Part25で追加。https://www.urablog.xyz/entry/2017/08/06/104652
    [HideInInspector]
    public float RuntimeValue;
    public void OnAfterDeserialize() {
        RuntimeValue = initialValue;
    }//デシリアライズ後に読み込む。
    public void OnBeforeSerialize() { }//シリアライズ前に読み込む。

}
