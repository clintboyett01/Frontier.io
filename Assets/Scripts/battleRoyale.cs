using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class battleRoyale : MonoBehaviour
{
    // Start is called before the first frame update
    ArrayHolder arrayHolder;
    public Text text;
    public GameObject youWon;
    bool won;
    float winersSpoils=5;
    void Start()
    {
        arrayHolder = FindObjectOfType<ArrayHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        string temp = "There are " + arrayHolder.scoreTracker.Count + "\nPlayers left!";
        text.text = temp;
        if (arrayHolder.scoreTracker.Count == 1)
        {
            youWon.SetActive(true);
            won = true;
        }
        if (won)
        {

            if (winersSpoils < 0)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                winersSpoils -= Time.deltaTime;
            }
        }
    }
}
