using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameManager gameManager;
    private float halfWidth;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Bounds bounds = renderer.bounds;
        foreach(Transform child in transform) {
            bounds.Encapsulate(child.GetComponent<SpriteRenderer>().bounds);
        }
        halfWidth = bounds.extents.x;
    }

    void Update()
    {
        transform.Translate(Vector3.left * gameManager.envVelocity * Time.deltaTime);
        if(startPosition.x - transform.position.x >= halfWidth) {
            transform.position = startPosition;
        }
    }
}
