using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable //Part31でInteractableクラスからの継承に変更。
{
    //public Signal contextOn;//Part30で追加。
    //public Signal contextOff;//Part30で追加。
    //public Signal context;//Part31でInteractableクラスに移動。
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    //public bool playerInRange;//Part31でInteractableクラスに移動。

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーがCollider2Dに触れていて、スペースキーを押したとき
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            //dialogBoxがヒエラルキーの中でアクティブならば、非アクティブにする。
            if (dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
                playerInRange = true;
            } else {
            //dialogBoxが非アクティブならば、アクティブにし、テキストを表示する。
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
        /*if (!playerInRange) {
            dialogBox.SetActive(false);
        }*/
    }

    //Part31でInteractableクラスに移動。

    //PlayerがCollider2Dに触れたときに作動。
    //引数をCollisionからotherに変えないと、Player以外のオブジェクトが触れたときにも作動してしまう。
    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger){
            //playerInRange = true;
            //contextOn.Raise();//Part30で追加。
            //context.Raise();
        }
    }*/

    //Part31でInteractableクラスに一部コピー。
    //PlayerがCollider2Dから離れたときに作動
    override public void OnTriggerExit2D(Collider2D other) {//Part33で上手く動作しなかったため、オーバーライドさせた。元にvirtual、子にoverrideが必要。publicでないとだめ。
        if (other.CompareTag("Player") && !other.isTrigger) {
            //contextOff.Raise();//Part30で追加。
            context.Raise();//Part31で無効にした。Interactableクラスとかぶるため。→Part33で復活。オーバーライドさせたため。
            playerInRange = false;//同上。
            dialogBox.SetActive(false);
        }
    }
}
