using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameManager manager;
    public GameObject[] fishes;

    void Start() {
        StartCoroutine(SpawnFish());
    }

    private IEnumerator SpawnFish() {
        while(true) {
            if(manager is null) {
                throw new System.Exception("Game Manager is null");
            }
            GameObject obj = Instantiate(fishes[UnityEngine.Random.Range(1, 4)], transform);
            Fish fish = obj.GetComponent<Fish>();
            fish.gm = manager;
            fish.Shift(UnityEngine.Random.Range(1, 4));
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.4f, 3f));
        }
    }
}
