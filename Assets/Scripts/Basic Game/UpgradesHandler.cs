using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerObj;
    Controller player;
    public int score;
    public float upgradeCost;
    public float upgradeMultiplier;
    public GameObject menuOpener;
    public GameObject upgradeMenu;
    public GameObject bulletUpgradeMenu;
    public GameObject towerUpgradeMenu;
    public GameObject healthUpgradeMenu;
    public GameObject menuCloser;
    public GameObject upgradeMenuImage;
    public GameObject bulletUpgradeMenuImage;
    public GameObject towerUpgradeMenuImage;
    public GameObject healthUpgradeMenuImage;
    public GameObject maxedTxt;
    GameObject scaleUpObject;
    GameObject scaleDownObject;
    GameObject scaleMenu;
    Player2DExample speedController;
    public bool inTeamMode;
    
    bool scaleUp = false;
    bool scaleDown = false;
    float scaleUpStatus;
    float scaleDownStatus;
    float maxTimer = 0;
    bool max;

    int speed;
    int bulletDamage;
    int bulletSpeed;
    int bulletFireRate;
    int bulletLife;
    int maxHp;
    int hpRegen;
    int towerHp;
    int towerBulletDamage;
    int towerBuildRate;
    float towerZone = 1;

    private void Start()
    {
        player = playerObj.GetComponent<Controller>();
        speedController = playerObj.GetComponent<Player2DExample>();
        player.towerZoneUpgrade = towerZone;
    }

    public void openUpgradeMenu()
    {
        //Debug.Log("it works?");
        menuCloser.SetActive(true);
        menuOpener.SetActive(false);
        upgradeMenu.SetActive(true);
        //scaleUpObject = upgradeMenuImage;
        //scaleMenu = upgradeMenu;
        //scaleUp = true;
        
    }



    public void closeUpgradeMenu()
    {
        menuOpener.SetActive(true);
        menuCloser.SetActive(false);
        upgradeMenu.SetActive(false);
        bulletUpgradeMenu.SetActive(false);
        towerUpgradeMenu.SetActive(false);
        healthUpgradeMenu.SetActive(false);
        //scaleDown =true;

    }

    public void upgradeHealth()
    {
        //scaleDownObject = scaleUpObject;
        //scaleUpObject = healthUpgradeMenu;
        healthUpgradeMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }

    public void upgradeBullet()
    {
        //scaleObject = bulletUpgradeMenu;
        bulletUpgradeMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }

    public void upgradeTower()
    {
        //scaleObject = towerUpgradeMenu;
        towerUpgradeMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }

    public void upgradeSpeed()
    {
        if(speed < 10)
        {
            speed++;
            spend();
            player.speedUpgrade = speed;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeHealthRegen()
    {
        if (hpRegen < 10)
        {
            hpRegen++;
            spend();
            player.regenSpeed = 4-(hpRegen/3);
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeHealtMax()
    {
        if (maxHp < 10)
        {
            maxHp++;
            spend();
            player.maxHP += maxHp;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeBulletFireRate()
    {
        if (bulletFireRate < 10)
        {
            bulletFireRate++;
            spend();
            player.ShootCooldown -= .04f;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeBulletDamage()
    {
        if (bulletDamage < 10)
        {
            bulletDamage++;
            spend();
            player.bulletDamageUpgrade = bulletDamage;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeBulletSpeed()
    {
        if (bulletSpeed < 10)
        {
            bulletSpeed++;
            spend();
            player.bulletSpeedUpgrade = bulletSpeed;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeBulletLife()
    {
        if (bulletLife < 10)
        {
            bulletLife++;
            spend();
            player.bulletLifeUpgrade = bulletLife;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeTowerHealth()
    {
        if (towerHp < 10)
        {
            towerHp++;
            spend();
            player.towerHpUpgrade = towerHp;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeTowerBulletDamage()
    {
        if (towerBulletDamage < 10)
        {
            towerBulletDamage++;
            spend();
            player.towerBulletDamageUpgrade = towerBulletDamage;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeTowerBuildRate()
    {
        if (towerBuildRate < 10)
        {
            towerBuildRate++;
            spend();
            player.BuildCoolDown -= 0.5f;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    public void upgradeTowerZone()
    {
        if (towerZone < 2.5)
        {
            towerZone+=.25f;
            spend();
            player.towerZoneUpgrade = towerZone;
            closeUpgradeMenu();
            menuOpener.SetActive(false);
        }
        else
        {
            max = true;
        }
    }

    void spend()
    {
        score -= (int)upgradeCost;
        upgradeCost += upgradeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (score > upgradeCost)
            {
                if (menuCloser.activeSelf)
                {
                    menuOpener.SetActive(false);
                }
                else
                {
                    menuOpener.SetActive(true);
                }
            }
            if (max)
            {
                if (maxTimer < 2)
                {
                    maxedTxt.SetActive(true);
                    maxTimer += Time.deltaTime;
                }
                else
                {
                    maxedTxt.SetActive(false);
                    max = false;
                    maxTimer = 0;
                }
            }
            speedController.upgradeSpeed = speed;
        

        //if (scaleUp)
        //{
        //    if (scaleStatus < .08)
        //    {
        //        float temp = (scaleStatus + Time.deltaTime) / scaleStatus; 
        //        scaleStatus += Time.deltaTime;
        //        scaleObject.GetComponentInChildren<RectTransform>().localScale *= temp;
        //    }
        //    else
        //    {
        //        scaleUp = false;
        //    }
        //}
        //if (scaleDown)
        //{
        //    if (scaleStatus > 0)
        //    {
        //        float temp = (scaleStatus - Time.deltaTime) / scaleStatus;
        //        scaleStatus -= Time.deltaTime;
        //        scaleObject.GetComponentInChildren<RectTransform>().localScale *= temp;
        //    }
        //    else
        //    {
        //        scaleMenu.SetActive(false);
        //        scaleDown = false;
        //    }
        //}
    }

  
}
