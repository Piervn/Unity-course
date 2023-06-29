using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour {

    public GameManager gameManager;
    public Rigidbody rb;
    public Animator anim;
    public float jumpForce = 300;
    private Vector3 speed;
    private float blend = 0f;

    private void Start() {
        anim = GetComponent<Animator>();
        speed = Vector3.up * jumpForce * Mathf.Abs(Physics.gravity.y) / 100;
    }
    
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space) && !gameManager.gameOver) {
            rb.velocity = speed;
        }

        blend = Mathf.Lerp(blend, rb.velocity.y / speed.magnitude, 0.3f);
        anim.SetFloat("Blend", blend);
    }
}
