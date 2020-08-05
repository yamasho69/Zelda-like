using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part28で作成。
public class SceneTransition : MonoBehaviour{

    [Header("New Scene Variables")]//Part41で追加。
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax;//Part41で追加。
    public Vector2 cameraNewMin;//Part41で追加。
    public VectorValue cameraMax;//Part41で追加。
    public VectorValue cameraMin;//Part41で追加。
    

    [Header("Transition Variables")]//Part41で追加。
    public GameObject fadeInPanel;//Part29で追加。
    public GameObject fadeOutPanel;
    public float fadeWait;


    //Part29で作成。
    private void Awake() {
        if(fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity)as GameObject;
            Destroy(panel, 1);//1秒後にパネルを破壊。
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            playerStorage.initialValue = playerPosition;
            //SceneManager.LoadScene(sceneToLoad);　Part29でコメントアウト
            StartCoroutine(FadeCo());
        }    
    }

    public IEnumerator FadeCo() {
        if (fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone) {
            yield return null;
        }
    }

    //Part41で作成。シーン移行があった際のカメラ位置のリセット。
    public void ResetCameraBounds() {
        cameraMax.initialValue = cameraNewMax;//cameraMaxの初期値をcameraNewMaxにする。
        cameraMin.initialValue = cameraNewMin;//同上。
    }
}
