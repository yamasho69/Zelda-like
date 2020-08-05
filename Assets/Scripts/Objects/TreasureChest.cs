using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Part32で作成。内容はPart33で記載開始。
public class TreasureChest : Interactable{

    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;//Part42で作成。

    [Header("Signals and Dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Signals and Dialog")]
    private Animator anime;

    // Start is called before the first frame update
    void Start(){
        anime = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;//Part42で追加。storedOpenにtrueならば、シーン遷移時にisOpenをtrueになり、宝箱が開き済みになる。
        if (isOpen) {
            anime.SetBool("opened",true);
        }
    }

    //Part33で、Signクラスからコピー。
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {//Part33宝箱を取ると、この条件が常に有効になり、Signが機能しない。
            if (!isOpen) {//箱を開ける
                OpenChest();
            } else {//箱をすでにあけている。
                ChestAlreadyOpen();
            }
        }
    }
    public void OpenChest() {
        //ウィンドウオープン
        dialogBox.SetActive(true);
        //ダイアログテキストを表示
        dialogText.text = contents.itemDescription;
        //インベントリにコンテンツを追加
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //Raise the signal to the player to animate
        raiseItem.Raise();
        //reise the context clue;
        context.Raise();
        isOpen = true;
        anime.SetBool("opened", true);
        storedOpen.RuntimeValue = isOpen;//Part42で作成。
    }
    public void ChestAlreadyOpen() {
        if (dialogBox == true) {
            //raise the signal to the player to stop animating
            raiseItem.Raise();
            //ダイアログテキストオフ
            //dialogBox.SetActive(false);//この行を有効にすると、宝箱を取った後にSignオブジェクトが上手く起動しない。
        }
    }

    override public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {//箱が空いている場合は作動しない。
            playerInRange = true;
            //contextOn.Raise();//Part30で追加。
            context.Raise();
        }
    }

    //PlayerがCollider2Dから離れたときに作動(Part33でオーバーライドを導入)
    override public void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {//箱が空いている場合は作動しない。
            //contextOff.Raise();//Part30で追加。
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }else if(other.CompareTag("Player") && !other.isTrigger) {
            dialogBox.SetActive(false);
        }
    }
}
