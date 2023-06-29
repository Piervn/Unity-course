using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWindow : MonoBehaviour
{
    public GameObject letterBackround;
    public GameObject[] letters = new GameObject[7];
    void Start()
    {
        for(int i = -3; i < 4; i++) {
            GameObject go = Instantiate(letterBackround, transform);
            Vector2Int vector = (Vector2Int.right * 170 * i) + (Vector2Int.down * 80 * ((i+3)%2));
            go.transform.localPosition = (Vector2)vector;
        }
    }
}
