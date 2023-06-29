using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody rb;
    private Vector3 offset;
    
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Bomb") {
            collision.gameObject.GetComponent<Bomb>().Explode();
            rb.velocity = Vector3.zero;
            offset = collision.gameObject.name == "TopBomb" ? Vector3.up : Vector3.down;
            rb.AddExplosionForce(2000, collision.gameObject.transform.position + (offset * 2), 20);
            gameManager.EndGame();
        }
    }
}
