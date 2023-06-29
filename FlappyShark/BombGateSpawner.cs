using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombGateSpawner : MonoBehaviour 
{
    public GameManager manager;
    public GameObject bombGate;

    private float bombsMaxFrequency = 1f;
    private float upperBound = 10f;
    private float lowerBound = -10f;


    void Start() {
        StartCoroutine(SpawnBombGate());
    }

    private IEnumerator SpawnBombGate() { 
        while(true) {
            if(manager is null || bombGate is null) {
                throw new System.Exception("Game Manager or Bomb Gate is null");
            }
            GameObject obj = Instantiate(bombGate, transform);
            BombGate bg = obj.GetComponent<BombGate>();
            bg.gm = manager;
            float shift = UnityEngine.Random.Range(lowerBound + (manager.distBetweenBombs / 2), upperBound - (manager.distBetweenBombs / 2));
            bg.Shift(shift);
            yield return new WaitForSeconds(Math.Max(bombsMaxFrequency, manager.bombsFrequency));
        }
    }
}
