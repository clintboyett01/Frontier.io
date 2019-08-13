using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModelColor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isInTeamMode;
    void Start()
    {
        getColor();
    }

    void getColor()
    {
        if (!isInTeamMode)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color ce = new Color32((byte)PlayerPrefs.GetInt("red"), (byte)PlayerPrefs.GetInt("green"), (byte)PlayerPrefs.GetInt("blue"), 255);
            sr.color = ce;
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color ce = Color.red;
            sr.color = ce;
        }
    }
   
}
