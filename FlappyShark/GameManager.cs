using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject bombSpawner;
    public GameObject bombGate;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI score;
    
    public bool gameStarted = false;
    public float envVelocity = 5f;
    
    // Bomby
    public float bombsFrequency = 3f;
    public float distBetweenBombs = 10f;

    // Game Factors
    public float deltaFrequency = 0.1f;
    public float deltaDistBetween = 0.1f;
    public float deltaVelocity = 0.1f;

    // Game
    public bool gameOver = false;


    private void Awake() {
        Physics.gravity = 3 * Vector3.up * -9.81f;
    }

    private void Update() {
        if(!gameStarted && Input.GetKeyDown(KeyCode.Space)) {
            StartGame();
        }
        if(gameStarted) {
            bombsFrequency -= deltaFrequency * Time.deltaTime;
            distBetweenBombs -= deltaDistBetween * Time.deltaTime;
            envVelocity += deltaVelocity * Time.deltaTime;
        }
    }

    public void StartGame() {
        gameStarted = true;
        player.rb.useGravity = true;
        GameObject obj = Instantiate(bombSpawner, player.transform.position + Vector3.right * 40f, Quaternion.identity);
        obj.GetComponent<BombGateSpawner>().manager = this;
        obj.GetComponent<BombGateSpawner>().bombGate = bombGate;
        startText.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
    }

    public void EndGame() {
        if(!gameOver) {
            Debug.Log("Game over!");
            gameOver = true;
        }
        Invoke("ReloadScene", 1f);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
