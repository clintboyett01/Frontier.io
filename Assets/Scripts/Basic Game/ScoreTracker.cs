using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreTracker : MonoBehaviour{

    public string color;
    public int score;
    public string teamName;
    Color32 color32;
    public bool isMainPlayer = false;
    EnemyCode enemy;
    UpgradesHandler UpgradesHandler;
    public GameObject upgrader;
    public bool inTeamMode;
    void Start()
    {
        
        color32 = GetComponent<SpriteRenderer>().color;
        color = color32.ToString();
        FindObjectOfType<ArrayHolder>().scoreTracker.Add(this);
        teamName = FindObjectOfType<StartUpCode>().getName();
        
        try
        {
            if (GetComponent<Controller>().mainPlayer)
            {
                isMainPlayer = true;
                teamName = PlayerPrefs.GetString("name");
                color = GetComponent<Controller>().color;
            }
           
        }
        catch(System.Exception e) { }
        if (isMainPlayer)
        {
            Debug.Log("it works");
            UpgradesHandler = upgrader.GetComponent<UpgradesHandler>();
            Debug.Log(UpgradesHandler.upgradeMultiplier);
        }
        else
        {
            enemy = GetComponent<EnemyCode>();
        }

    }

    public void addScore()
    {
        score += 5;
        
        UpgradesHandler.score += 5;
        if (inTeamMode)
        {
            StartUpCode.redTeamScore += 5;
        }
    }

   

}

public class score : IComparer<ScoreTracker>
{
    public int Compare(ScoreTracker x, ScoreTracker y)
    {
        return y.score.CompareTo(x.score);
    }
}