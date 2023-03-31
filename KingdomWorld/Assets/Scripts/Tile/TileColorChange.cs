using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorChange : MonoBehaviour
{
    public void ChangeRedColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ChangeWhiteColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
