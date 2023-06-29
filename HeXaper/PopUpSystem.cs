using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpGameOver;
    public GameObject popUpGameWon;
    public Animator gameOverAnim;
    public Animator gameWonAnim;

    public void PopUp() {
        if(HexGrid.Instance.gameOver) {
            popUpGameOver.SetActive(true);
            gameOverAnim.SetTrigger("Pop");
        }
        else {
            popUpGameWon.SetActive(true);
            gameWonAnim.SetTrigger("Pop");
        }
            
    }

}
