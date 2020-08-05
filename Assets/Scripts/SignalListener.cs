using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Part23で作成。
public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;//UnityEventはコンポーネント上に追加される。　https://www.urablog.xyz/entry/2016/09/11/080000
    public void OnSignalRaised() {
        signalEvent.Invoke();
    }

    private void OnEnable() {//アクティブになったときに呼ばれるメソッド https://www.hanachiru-blog.com/entry/2020/01/07/161511
        signal.RegisterListener(this);
    }
    private void OnDisable() {
        signal.DeRegisterListener(this);//非アクティブになったときに呼ばれるメソッド
    }
}
