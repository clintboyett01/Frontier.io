using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text myText;
    float timeLeft = 0;
    public GameObject player;
    public GameObject textObj;
    ScoreTracker st;
    string txt = "";

    void Start()
    {
        myText = GetComponent<Text>();
        st = player.GetComponent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft < 0)
        {
            FindObjectOfType<ArrayHolder>().scoreTracker.Sort(new score().Compare);
            txt = 1 + FindObjectOfType<ArrayHolder>().scoreTracker.IndexOf(st) + ". " + st.teamName + " " + st.score;
            myText.text = txt;
            timeLeft = 1;

        }
        timeLeft -= Time.deltaTime;
    }
}
