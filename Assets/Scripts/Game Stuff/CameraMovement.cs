using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    //カメラには必ずrigidbody2dを付けること。付けないと遊びが出来てしまう。
    [Header("Position Variables")]//Part41で追加。
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition; //カメラの動く範囲を制御ための数値(画面右上)
    public Vector2 minPosition; //カメラの動く範囲を制御ための数値(画面左下)
    [Header("Animator")]//Part41で追加。
    public Animator anim;

    [Header("Position Reset")]//Part41で追加。
    public VectorValue camMin;//Part41で追加。
    public VectorValue camMax;//Part41で追加。

    // Start is called before the first frame update
    void Start(){
        maxPosition = camMax.initialValue;//Part41
        minPosition = camMin.initialValue;//Part41
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, -10);//Part28で追加。z座標がPart41終了までtarget.position.yとなっており間違っていた。
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x,
                                                 target.position.y,
                                                 transform.position.z);

            //第一引数の数値を第二引数以上第三引数以下に固定
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                                           minPosition.x,
                                           maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                               minPosition.y,
                               maxPosition.y);

            transform.position = Vector3.Lerp(transform.position,
                                              targetPosition, smoothing);
        }
    }

    //Part40で追加。
    public void Beginkick() {
        anim.SetBool("kick_active", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo() {
        yield return null;
        anim.SetBool("kick_active", false);
    }
}
