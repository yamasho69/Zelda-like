using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //カメラには必ずrigidbody2dを付けること。付けないと遊びが出来てしまう。
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition; //カメラの動く範囲を制御ための数値(画面右上)
    public Vector2 minPosition; //カメラの動く範囲を制御ための数値(画面左下)

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.y);//Part28で追加。
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}
