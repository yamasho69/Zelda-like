using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part23で作成。
[CreateAssetMenu] //CreateAssetMenuについて https://www.urablog.xyz/entry/2017/03/28/235739
public class Signal : ScriptableObject{
    //リストを作成。
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener) {
        listeners.Add(listener);//リストに追加
    }
    public void DeRegisterListener(SignalListener listener) {
        listeners.Remove(listener);//リストから削除
    }
}
