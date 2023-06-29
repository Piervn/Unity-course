using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameManager gm;
    private float destructionX = -400f;

    private void Start() {
        int s = UnityEngine.Random.Range(4, 10);
        transform.localScale = new Vector3(s, s, s);
    }

    private void Update() {
        transform.Translate(Vector3.left * (gm.envVelocity + 10f) * Time.deltaTime);

        if(transform.position.x < destructionX) {
            Destroy(gameObject);
        }
    }

    public void Shift(int layer) {
        transform.position += Vector3.up * UnityEngine.Random.Range(0, 20f * layer);
        switch(layer) {
            case 1:
                transform.position += Vector3.forward * 25f;
                break;
            case 2:
                transform.position += Vector3.forward * 40f + Vector3.down * 14f;
                break;
            case 3:
                transform.position += Vector3.forward * 60f;
                break;
            default:
                break;
        }
    }
}
