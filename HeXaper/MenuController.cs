using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void LoadSmallBoard() {
        if(HexGrid.bombs == 0)
            HexGrid.bombs = 5;
        else {
            HexGrid.bombs *= 3;
            HexGrid.bombs += 5;
        }
        SceneManager.LoadScene("Small");
    }
    public void LoadMediumBoard() {
        if(HexGrid.bombs == 0)
            HexGrid.bombs = 12;
        else {
            HexGrid.bombs *= 14;
            HexGrid.bombs += 12;
        }
            
        SceneManager.LoadScene("Medium");
    }
    public void LoadLargeBoard() {
        if(HexGrid.bombs == 0)
            HexGrid.bombs = 80;
        else {
            HexGrid.bombs *= 50;
            HexGrid.bombs += 80;
        }
        SceneManager.LoadScene("Large");
    }
}
