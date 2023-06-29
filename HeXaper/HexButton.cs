using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HexButton : MonoBehaviour, IPointerClickHandler {

    public Vector2Int vector;
    public Button button;
    public Sprite[] hexes = new Sprite[8];
    public Sprite flag;
    public Sprite cover;
    public int boardVal;
    private bool flaged = false;
    private bool covered = true;

    public void OnPointerClick(PointerEventData eventData) {
        if(HexGrid.Instance.gameOver || HexGrid.Instance.gameWon)
            return;
        if(eventData.button == PointerEventData.InputButton.Left)
            UncoverField();
        else if(eventData.button == PointerEventData.InputButton.Right)
            PutFlag();
    }
    private void ChangeButtonImage() {
        if(flaged || !covered)
            return;
        button.image.sprite = hexes[boardVal];
        covered = false;
        ColorBlock colorVar = button.colors;
        colorVar.highlightedColor = new Color(255f, 255f, 255f);
        colorVar.pressedColor = new Color(255f, 255f, 255f);
        button.colors = colorVar;
    }
    private void UncoverField() {
        this.ChangeButtonImage();
        if(boardVal == 7 && !flaged) {
            UncoverBombs();
            HexGrid.Instance.gameOver = true;
            PopUpSystem gameOver = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PopUpSystem>();
            gameOver.PopUp();
        }
        if(WinCondition()) {
            HexGrid.Instance.gameWon = true;
            PopUpSystem gameWon = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PopUpSystem>();
            gameWon.PopUp();
        }
        if(boardVal != 0 || flaged)
            return;
        if(!flaged)
            boardVal = -1;
        Vector2Int field1 = vector - new Vector2Int(1, 0);
        Vector2Int field2 = vector - new Vector2Int(vector.y % 2, -1);
        Vector2Int field3 = vector + new Vector2Int((vector.y + 1) % 2, 1);
        Vector2Int field4 = vector + new Vector2Int(1, 0);
        Vector2Int field5 = vector + new Vector2Int((vector.y + 1) % 2, -1);
        Vector2Int field6 = vector - new Vector2Int(vector.y % 2, 1);
        Vector2Int[] neighbours = { field1, field2, field3, field4, field5, field6 };
        for(int i = 0; i < 6; i++) {
            if(HexGrid.Instance.hexDict.ContainsKey(neighbours[i])) {
                if(HexGrid.Instance.hexDict[neighbours[i]].boardVal != -1 && HexGrid.Instance.hexDict[neighbours[i]].flaged == false) {
                    HexGrid.Instance.hexDict[neighbours[i]].UncoverField();
                }       
            }
        }
    }
    private void UncoverBombs() {
        foreach(var item in HexGrid.Instance.hexDict) {
            if(item.Value.boardVal == 7) {
                item.Value.button.image.sprite = hexes[7];
                ColorBlock colorVar = button.colors;
                colorVar.highlightedColor = new Color(255f, 255f, 255f);
                colorVar.pressedColor = new Color(255f, 255f, 255f);
                button.colors = colorVar;
            } 
        }
    }
    private void PutFlag() {
        if(flaged == true) {
            HexGrid.flags -= 1;
            BombsCounter.Instance.UpdateBombsNum();
            button.image.sprite = cover;
            flaged = false;
            return;
        }
        if(covered == false)
            return;
        HexGrid.flags += 1;
        BombsCounter.Instance.UpdateBombsNum();
        button.image.sprite = flag;
        flaged = true;
        ColorBlock colorVar = button.colors;
        colorVar.highlightedColor = new Color(255f, 255f, 255f);
        colorVar.pressedColor = new Color(255f, 255f, 255f);
        button.colors = colorVar;
    }
    private bool WinCondition() {
        int coveredTiles = 0;
        foreach(var item in HexGrid.Instance.hexDict) {
            if(item.Value.covered)
                coveredTiles += 1;
        }
        if(coveredTiles == HexGrid.bombs)
            return true;
        else
            return false;
    }
}


