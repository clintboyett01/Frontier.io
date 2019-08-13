using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject privacyMenu;
    public GameObject infoMenu;
    public GameObject tutorial;
    public GameObject tutorialCloseButton;
    public Transform camera;
    float lastRot = 0;

    bool tutorialActive = false;
    bool spinToOptions;
    bool spinToPrivacy;
    bool spinToMain;
    bool spinToInfo;
 

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("NotFirstTimePlaying") == 0)
        {
            PlayerPrefs.SetInt("NotFirstTimePlaying", 1);
            tutorial.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Game");
        }

    }

    public void StartBattleRoyale()
    {
        if (PlayerPrefs.GetInt("NotFirstTimePlaying") == 0)
        {
            PlayerPrefs.SetInt("NotFirstTimePlaying", 1);
            tutorial.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Battle Royale");

        }
    }

    public void StartTeamMode()
    {
        if (PlayerPrefs.GetInt("NotFirstTimePlaying") == 0)
        {
            PlayerPrefs.SetInt("NotFirstTimePlaying", 1);
            tutorial.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Team Mode");
        }
    }


    public void toOptions()
    {
        mainMenu.SetActive(false);
        spinToOptions = true;
    }

    public void toPrivacy()
    {
        mainMenu.SetActive(false);
        spinToPrivacy = true;
    }

    public void toInfo()
    {
        mainMenu.SetActive(false);
        spinToInfo = true;
        
    }

    public void toMainMenu()
    {
        optionsMenu.SetActive(false);
        infoMenu.SetActive(false);
        privacyMenu.SetActive(false);
        spinToMain = true;
    }

    
    

    public void Update()
    {
       
        if (spinToOptions == true)
        {
            if (lastRot < 1.5)
            {
                lastRot += Time.deltaTime;
                camera.transform.Rotate(0, Time.deltaTime * 60, 0);
            }
            else
            {
                spinToOptions = false;
                optionsMenu.SetActive(true);
            }
        }
        if (spinToPrivacy == true)
        {
            if (lastRot < 1.5)
            {
                lastRot += Time.deltaTime;
                camera.transform.Rotate(0, Time.deltaTime * 60, 0);
            }
            else
            {
                spinToPrivacy = false;
                privacyMenu.SetActive(true);
            }
        }
        if (spinToMain == true)
        {
            if (lastRot > 0)
            {
                lastRot -= Time.deltaTime;
                camera.transform.Rotate(0,  - Time.deltaTime * 60, 0);
            }
            else
            {
                spinToMain= false;
                mainMenu.SetActive(true);
            }
        }
        if (spinToInfo == true)
        {
            if (lastRot < 3)
            {
                lastRot += Time.deltaTime;
                camera.transform.Rotate(0, Time.deltaTime * 60, 0);
            }
            else
            {
                spinToInfo = false;
                infoMenu.SetActive(true);
            }
        }
        
    }

    public void exit()
    {
        Application.Quit();
    }
}
