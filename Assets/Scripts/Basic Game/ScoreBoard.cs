using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    //team[] teams;
    //team[] topFive;
    ScoreTracker[] topFive;
    public Text myText;
    string topFiveTxt = "";
    float timeLeft = 0;
    void Start()
    {
        myText = GetComponent<Text>();
        firstTopFiveTeams();
    }

    // Update is called once per frame
    void Update()
    {
        //teams = getTeams();
        //topFive = findTopFiveTeams();
        findTopFiveTeams();
        
        if (timeLeft < 0) {
            topFiveTxt = "";
        for (int j = 1; j <= 5; j++)
        {

            topFiveTxt += j + ". " + topFive[j - 1].teamName + " " + topFive[j - 1].score + "\n";
            //klnmlono
            
        }
            timeLeft = 1;
        
    }
        timeLeft -= Time.deltaTime;
     //   Debug.Log(topFiveTxt.ToString()+"is the score");
        myText.text = topFiveTxt;
    }

    void firstTopFiveTeams()
    {
        topFive = new ScoreTracker[5];
        for(int k = 0; k < 5; k++) {
            topFive[k] = FindObjectOfType<ScoreTracker>();

        }
    }


    void findTopFiveTeams()
    {
        
        FindObjectOfType<ArrayHolder>().scoreTracker.Sort(new score().Compare);

        ScoreTracker[] st = FindObjectOfType<ArrayHolder>().scoreTracker.ToArray();

        for (int k = 0; k < 5; k++)
        {
            topFive[k] = st[k]; // this error is caused, because one score tracher is spawened each frame. for the first 4 frames, there are not enough trackers to fill the top five array.
        }


        
    }

    //team[] findTopFiveTeams()
    //{
    //    team trashTeam = new team();
    //    trashTeam.teamDie();//to disregard this team as a team in game
    //    team[] top = { trashTeam, trashTeam, trashTeam, trashTeam, trashTeam };
    //    foreach (team t in teams)
    //    {
    //        for (int k = 0; k < 5; k++)
    //        {
    //            if (t.score > top[k].score)
    //            {
    //                for (int h = k; h < 4; h++)
    //                {
    //                    top[h] = top[h + 1];
    //                }
    //                top[k] = t;

    //            }
    //        }
    //    }
    //    return top;
    //}

    //team[] getTeams()
    //{
    //    Object[] g = FindObjectsOfType(typeof(EnemyCode));
    //    team[] t = new team[g.Length];
    //    for (int h = 0; h < g.Length; h++)
    //    {
    //        EnemyCode temp = (EnemyCode)g[h];
    //        t[h] = temp.team;
    //    }
    //    return t;
    //}
}


