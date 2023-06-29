using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombGate : MonoBehaviour
{
    public GameManager gm;
    private float destructionX = -60f;
    private bool scored = false;

    private void Update() {
        transform.Translate(Vector3.left * gm.envVelocity * Time.deltaTime);
        if(!scored && transform.position.x < 0) {
            gm.score.text = (int.Parse(gm.score.text) + 1).ToString();
            scored = true;
        }
        if (transform.position.x < destructionX) Destroy(gameObject);
    }

    public void Shift(float shift) {
        transform.position += Vector3.up * shift;
        transform.GetChild(0).transform.position += Vector3.up * gm.distBetweenBombs / 2;
        transform.GetChild(1).transform.position += Vector3.down * gm.distBetweenBombs / 2;
    }
}
