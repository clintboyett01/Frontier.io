using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
    public Color col;
    public int num;
    public float size;
    public void setColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = col;
        Transform tr = GetComponent<Transform>();
        tr.localScale = new Vector3(1 * size, 1 * size, 1 * size);
    }
    public void tow(int deadTowNum)
    {
        if (num == deadTowNum)
        {
            Destroy(gameObject);
        }
    }
}
