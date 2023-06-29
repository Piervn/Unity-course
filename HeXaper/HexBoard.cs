using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBoard {
    int height;
    int width;
    int bombs; //liczba bomb mniejsza od height x width
    public int[,] board; //1-6 pola wokó³ bomby, 7 bomba

    public HexBoard(int h, int w, int b) {
        height = h;
        width = w;
        bombs = b;
        board = new int[height, width];
        FillBoardRandomly();
    }
    public void FillBoardRandomly() {
        while(bombs > 0) {
            DrawTileForBomb();
            bombs--;
        }
        PutRightNumbers();
    }
    private void DrawTileForBomb() {
        int xr = Random.Range(0, height);
        int yr = Random.Range(0, width);
        if(board[xr, yr] == 0) {
            board[xr, yr] = 7;
            return;
        }
        else {
            for(int i = 1; i <= height; i++) {
                for(int j = 1; j <= width; j++) {
                    if(board[(xr + i) % height, (yr + j) % width] == 0) {
                        board[(xr + i) % height, (yr + j) % width] = 7;
                        return;
                    }
                }
            }
            Debug.Log("Not_found_tile");
        }
    }
    private void PutRightNumbers() {
        for(int i = 0; i < height; i++) {
            for(int j = 0; j < width; j++) {
                if(board[i, j] != 7) {
                    board[i, j] = CountBombs(i, j);
                }
            }
        }
    }
    private int CountBombs(int i, int j) {
        int result = 0;
        if(i != 0 && i != (height - 1) && j != 0 && j != (width - 1)) {
            if(board[i + 1, j] == 7)
                result++;
            if(board[i - 1, j] == 7)
                result++;
            if(j % 2 == 0) {
                if(board[i + 1, j + 1] == 7)
                    result++;
                if(board[i, j + 1] == 7)
                    result++;
                if(board[i + 1, j - 1] == 7)
                    result++;
                if(board[i, j - 1] == 7)
                    result++;
            }
            else {
                if(board[i - 1, j + 1] == 7)
                    result++;
                if(board[i - 1, j - 1] == 7)
                    result++;
                if(board[i, j + 1] == 7)
                    result++;
                if(board[i, j - 1] == 7)
                    result++;
            }
        }
        else if(i == 0 && j != 0 && j != width - 1) {
            if(board[i, j - 1] == 7)
                result++;
            if(board[i, j + 1] == 7)
                result++;
            if(board[i + 1, j] == 7)
                result++;
            if(j % 2 == 0) {
                if(board[i + 1, j - 1] == 7)
                    result++;
                if(board[i + 1, j + 1] == 7)
                    result++;
            }
        }
        else if(i == height - 1 && j != 0 && j != width - 1) {
            if(board[i, j - 1] == 7)
                result++;
            if(board[i, j + 1] == 7)
                result++;
            if(board[i - 1, j] == 7)
                result++;
            if(j % 2 == 1) {
                if(board[i - 1, j - 1] == 7)
                    result++;
                if(board[i - 1, j + 1] == 7)
                    result++;
            }
        }
        else if(j == 0) {
            if(i != 0 && i != height - 1) {
                if(board[i, j + 1] == 7)
                    result++;
                if(board[i + 1, j + 1] == 7)
                    result++;
                if(board[i + 1, j] == 7)
                    result++;
                if(board[i - 1, j] == 7)
                    result++;
            }
            else if(i == 0) {
                if(board[i, j + 1] == 7)
                    result++;
                if(board[i + 1, j + 1] == 7)
                    result++;
                if(board[i + 1, j] == 7)
                    result++;
            }
            else {
                if(board[i, j + 1] == 7)
                    result++;
                if(board[i - 1, j] == 7)
                    result++;
            }
        }
        else {
            if(i != 0 && i != height - 1) {
                if(board[i, j - 1] == 7)
                    result++;
                if(board[i + 1, j - 1] == 7)
                    result++;
                if(board[i + 1, j] == 7)
                    result++;
                if(board[i - 1, j] == 7)
                    result++;
            }
            else if(i == 0) {
                if(board[i, j - 1] == 7)
                    result++;
                if(board[i + 1, j - 1] == 7)
                    result++;
                if(board[i + 1, j] == 7)
                    result++;
            }
            else {
                if(board[i, j - 1] == 7)
                    result++;
                if(board[i - 1, j] == 7)
                    result++;
            }
        }
        return result;
    }
}
