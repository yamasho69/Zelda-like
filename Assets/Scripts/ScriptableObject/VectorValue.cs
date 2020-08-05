using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part28で作成。初期値をデフォルト値にするクラス？
[CreateAssetMenu]
public class VectorValue : ScriptableObject,ISerializationCallbackReceiver{

    [Header("Value running in game")]//Part41で追加。
    public Vector2 initialValue;

    [Header("Value by default when starting")]//Part41で追加。
    public Vector2 defaultValue;

    public void OnAfterDeserialize() { initialValue = defaultValue; }

    public void OnBeforeSerialize() { }
}
