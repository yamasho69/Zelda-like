using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Part45で作成。
public class Projectile : MonoBehaviour{

    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myrigidbody2D;
    
    // Start is called before the first frame update
    void Start(){
        myrigidbody2D = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update(){
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel) {
        myrigidbody2D.velocity = initialVel * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Destroy(this.gameObject);
    }
}
