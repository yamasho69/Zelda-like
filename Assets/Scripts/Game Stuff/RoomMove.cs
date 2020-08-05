using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;//Part9で追加
    public string placeName;//Part9で追加
    public GameObject text;//Part9で追加
    public Text placeText;//Part9で追加

    // Start is called before the first frame update
    void Start()
    {
        //CameraMovementクラスを取り込む
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Playerタグを持ったものが衝突した場合
        if(other.CompareTag("Player")&&!other.isTrigger) {//&&!other.isTriggerはPart26で記述。1度の衝突のみ検知。
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            //Part9で追加。テキスト表示が必要な場合に機能する。
            if (needText) {
                StartCoroutine(placeNameCo());//コルーチンを使用。https://www.sejuku.net/blog/83712
            }
        }
    }

    //Part9で追加。
    private IEnumerator placeNameCo() {//IEnumerator　関数名でコルーチンメソッド
        text.SetActive(true);//テキストをオンにする。
        placeText.text = placeName;//テキストにplaceNameを表示する。
        yield return new WaitForSeconds(2f);//2秒待つ
        text.SetActive(false);//テキストをオフにする。
    }
}
