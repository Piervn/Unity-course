using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsCounter : MonoBehaviour
{
    public static BombsCounter Instance {
        get; private set;
    }
    public Text number;
    public void Start() {
        Instance = this;
        UpdateBombsNum();
    }
    public void UpdateBombsNum() {
        int tmp = HexGrid.bombs - HexGrid.flags;
        if(tmp >= 0) {
            number.color = Color.black;
            number.text = tmp.ToString();
        }
        else {
            number.text = "0";
            number.color = Color.red;
        }
    }
}
