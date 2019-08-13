using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUpCode : MonoBehaviour
{
    public GameObject enemyPlayer;
    public float xMaxRange;
    public float xMinRange;
    public float yMaxRange;
    public float yMinRange;
    public static Queue<Color> colors = new Queue<Color>();
    string colorList = "0074d9 7fdbff 39cccc 3d9970 2ecc40 01ff70 ffdc00 ff851b ff4136 85144b f012be b10dc9 800000 0174d9 7fdcff 3acccc 3e9970 2fcc40 02ff70 ffdc01 ff851c ff4137 86144b f112be b00dc9 800001";
    public static int towNum = 1;
    public  string[] namesArray;
    public static Queue<string> names = new Queue<string>();
    public  int arrayIndex = 0;
    string namesList = "Sandwich_Protector Dora_the_Destroya Taste_the_Rambo Steve sixsicksheep Scooby_Dooku Rescuetoast Dodge_Is_Pointless Aim_Is_Futile Communism NOT_A_VIRUS.exe comrade2012 WarHawk Kladenstien Audacity BeoWulf IceQueen Mistake SomeTacos AllGoodNamesRGone Error404 ElNino DisasterMaster ๖ۣۜǤнσsτ༻ḲḭⱢⱢΣṜ ⚔ƁᙈββŁΣßΛĻŽ༻⊱遊㊣ ༺♕〘Ł€g€ŇĐ〙♕༻ DRAGON afdad TIGeR yolo zoro oneshot ʟᴜᴢᴀʀᴅ HUNTER asgard Viper Firehan █▬█-█-▀█▀ ░S░K░I░L░L░S░ Heяø Ѵσятєჯ ≽ܫ≼】ᖫ✧ᗷØŁŦ✧ᖭ";
    public GameObject quitMenu;
    public GameObject gameUI;
    public GameObject config1;
    public GameObject config2;
    public GameObject redTeamWon;
    public GameObject blueTeamWon;
    public static int blueTeamScore;
    public static int redTeamScore;
    public bool inTeamMode;
    public float endTimer = 10;
    public Text teamScoreBoard;
    public bool endGame = false;
    public bool battleRoyale;
    public string getName()
    {
        return names.Dequeue();
    }

    public void addName(string name)
    {
        names.Enqueue(name);
    }



    public void addColor(Color color)
    {
        colors.Enqueue(color);
    }

    public void nameQueueFill()
    {
        namesArray = namesList.Split(' ');
        for (int t = 0; t < namesArray.Length; t++)
        {
            string tmp = namesArray[t];
            int r = Random.Range(t, namesArray.Length);
            namesArray[t] = namesArray[r];
            namesArray[r] = tmp;
        }
        foreach (string name in namesArray){
            
            names.Enqueue(name);
        }
    }



    void Start()
    {
        Controller = FindObjectOfType<Controller>();
        colorQueueFill();
        nameQueueFill();
        if (PlayerPrefs.GetInt("altranateControls") == 0)
        {
            config1.SetActive(false);
            config2.SetActive(true);
            if (PlayerPrefs.GetInt("joystickFixed") == 1)
            {
                config2.GetComponentInChildren<VariableJoystick>().isFixed = true;
            }
            else
            {
                config2.GetComponentInChildren<VariableJoystick>().isFixed = false;
            }
        }
        else
        {
            config1.SetActive(true);
            config2.SetActive(false);
            if (PlayerPrefs.GetInt("joystickFixed") == 1)
            {
                config1.GetComponentInChildren<VariableJoystick>().isFixed = true;
            }
            else
            {
                config1.GetComponentInChildren<VariableJoystick>().isFixed = false;
            }
        }
        if (battleRoyale)
        {
            for (int y = 0; y < 24; y++)
            {
                summonEnemy();
            }
        }
        else if(inTeamMode)
        {
            for(int y = 0; y<7; y++)
            {
                summonEnemyTeamMode(Color.red);
            }
            for (int y = 0; y < 8; y++)
            {
                summonEnemyTeamMode(Color.blue);
            }
        }
        else
        {
            summonEnemy();
            summonEnemy();
            summonEnemy();
            summonEnemy();
            summonEnemy();
            summonEnemy();
            summonEnemy();
        }
    }

    byte numberize(string str)
    {
        byte f;
        byte s;
        switch (str.Substring(0, 1))
        {
            case "a": f = 10;  break;
            case "b": f = 11; break;
            case "c": f = 12; break;
            case "d": f = 13; break;
            case "e": f = 14; break;
            case "f": f = 15; break;
            default: f = byte.Parse( str.Substring(0,1)); break;
        }
        switch (str.Substring(1, 1))
        {
            case "a": s = 10; break;
            case "b": s = 11; break;
            case "c": s = 12; break;
            case "d": s = 13; break;
            case "e": s = 14; break;
            case "f": s = 15; break;
            default: s = byte.Parse(str.Substring(1, 1)); break;
        }
        f *= 16;
        return (byte)(f + s);
    }

    void colorQueueFill()
    {
        string[] colorListArray = colorList.Split(' ');
        
        foreach(string el in colorListArray) { 
            string tempCol = el;
            string sr = tempCol.Substring(0,2);
            string sg = tempCol.Substring(2, 2);
            string sb = tempCol.Substring(4, 2);
            byte r = numberize(sr);
            byte g = numberize(sg);
            byte b = numberize(sb);
            Color32 cole = new Color32(r,g,b,255);
            colors.Enqueue(cole);
        }
    }
    public void respawn()
    {
        DiedTimeToRespawn = true;
    }
    bool DiedTimeToRespawn = false;
    public float respawnTime = 10;
    Controller Controller;
    void Update()
    {
        
        if (DiedTimeToRespawn)
        {
            if(respawnTime < 0)
            {
                if (battleRoyale)
                {
                    SceneManager.LoadScene("Main Menu");
                }
                
                Controller.respawn();
                DiedTimeToRespawn = false;
                respawnTime = 10;
                
            }
            else
            {
                Debug.Log(respawnTime);
                respawnTime -= Time.deltaTime;
            }
        }
        
        if (battleRoyale)
        {
            
           
        }
        else if(inTeamMode)
        {
            string tempTxt = "Red team score - " + redTeamScore + "\n" + "Blue team score - " + blueTeamScore;
            teamScoreBoard.text = tempTxt;
            if (redTeamScore > 10000)
            {
                redTeamWon.SetActive(true);
                endGame = true;
            }
            if (blueTeamScore > 10000)
            {
                blueTeamWon.SetActive(true);
                endGame = true;
            }
        }
        else
        {
            if (GetEnemyNum() < 17)
            {
                summonEnemy();
            }
            if (colors.Count < 1)
            {
                colorQueueFill();
            }
        }
        if (names.Count < 1)
        {
            nameQueueFill();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
            gameUI.SetActive(false);
        }
        if (endGame)
        {
            if (endTimer < 0)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                endTimer -= Time.deltaTime;
            }
        }
    }

    void summonEnemy()
    {
        Vector2 pos = new Vector2(Random.Range(xMinRange, xMaxRange), Random.Range(yMinRange, yMaxRange));
        Instantiate(enemyPlayer, pos, Quaternion.identity);
    }

    public void summonEnemyTeamMode(Color color)
    {
        Vector2 pos = new Vector2(Random.Range(xMinRange, xMaxRange), Random.Range(yMinRange, yMaxRange));
        enemyPlayer.GetComponent<EnemyCode>().teamModeColor = color;
        enemyPlayer.GetComponent<EnemyCode>().inTeamMode = true;
        Instantiate(enemyPlayer, pos, Quaternion.identity);
    }

    int GetEnemyNum()
    {
        Object[] g = FindObjectsOfType(typeof(EnemyCode));
        return g.Length;
    }
    
}


