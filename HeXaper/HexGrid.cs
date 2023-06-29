using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HexGrid : MonoBehaviour {
    public static HexGrid Instance {
        get; private set;
    }
    public HexBoard hexBoard;
    public HexButton hexPrefab;
    public int height;
    public int width;
    public static int bombs;
    public static int flags = 0;
    public static bool backed = false;
    public int[] factors = new int[3];
    public bool gameOver = false;
    public bool gameWon = false;
    public IDictionary<Vector2Int, HexButton> hexDict = new Dictionary<Vector2Int, HexButton>();

    IEnumerator Start() {
        Instance = this;
        hexBoard = new HexBoard(height, width, bombs);
        for(int i = 0; i < height; i++) {
            for(int j = -(width / 2); j < (width / 2) + 1; j++) {
                HexButton go = Instantiate(hexPrefab, transform);
                Vector2Int vector = (Vector2Int.down * i * factors[0]) + (Vector2Int.right * j * factors[1]) + (Vector2Int.down * ((j + (width / 2) + 1) % 2) * factors[2]);
                go.transform.localPosition = (Vector2)vector;
                go.vector = new Vector2Int(i, j + (width/2));
                go.boardVal = hexBoard.board[i, j + (width/2)];
                hexDict.Add(new Vector2Int(i, j + (width/2)), go);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Update() {
        bool quit = Input.GetKey(KeyCode.Escape);
        if(quit)
            BackToMenu();
    }
    public void RestartGrid() {
        HexGrid.flags = 0;
        BombsCounter.Instance.UpdateBombsNum();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu() {
        HexGrid.bombs = 0;
        HexGrid.flags = 0;
        HexGrid.backed = true;
        SceneManager.LoadScene("MainMenu");
    }
}



