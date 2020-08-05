using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Part13で作成
public class Pot : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash() {
        anim.SetBool("smash",true);
        StartCoroutine(breakCo());//コルーチンメソッドの呼び方
    }

    //コルーチン処理
    //https://www.sejuku.net/blog/83712
    IEnumerator breakCo() {
        yield return new WaitForSeconds(.3f);　//0.3秒停止
        this.gameObject.SetActive(false);
    }
}
