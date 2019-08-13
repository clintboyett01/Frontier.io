using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public string color;
    bool checkOnce = false;
    bool checkTwice = false;
    // Use this for initialization
    void Start () {
        checkOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (checkOnce)
        {
            if (checkTwice)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                Color32 c = sr.color;
                color = c.ToString();
                checkOnce = false;
             
            }
            checkTwice = true;
        }
	}
}
