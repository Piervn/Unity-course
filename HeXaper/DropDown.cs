using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public void Update() {
        bool quit = Input.GetKey(KeyCode.Escape);
        if(quit)
            Application.Quit();
    }
    public void HandleInputData(int val) {
        HexGrid.bombs = val;
    }
}
