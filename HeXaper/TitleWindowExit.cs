using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWindowExit : MonoBehaviour
{
    public GameObject window;
    public Animator titleWindowAnim;

    public void Start() {
        if(HexGrid.backed)
            this.transform.position = new Vector3(0, -2000, 0);
    }
    public void SwipeLeft() {
        window.SetActive(true);
        titleWindowAnim.SetTrigger("swipeleft");
    }
}
