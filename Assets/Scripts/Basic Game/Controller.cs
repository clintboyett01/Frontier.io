using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{

    public GameObject tower;
    public GameObject diedMenu;
    public GameObject shot;
    public Color MyColor;
    public Sprite saucer;
    public Button Button;

    public float speed = 3f;
    public float BuildCoolDown = 10;
    public float ShootCooldown = 0.5f;
    public float turnSensitivity = -2f;
    public float regenSpeed;
    public float towerToCloseDistance;
    public float shotOffset;

    public int maxHP = 10;
    public int hp = 10;

    public string color;

    public bool mainPlayer = true;
    public bool flyingSaucer;
    public bool inTeamMode;
    public bool battleRoyale;

    //Upgrades
    public float towerZoneUpgrade;
    public float hpRegenUpgrade;
    public int speedUpgrade;
    public int bulletDamageUpgrade;
    public int bulletSpeedUpgrade;
    public int bulletFireRateUpgrade;
    public int bulletLifeUpgrade;
    public int maxHpUpgrade;
    public int towerHpUpgrade;
    public int towerBulletDamageUpgrade;
    public int towerBuildRateUpgrade;


    Color32 c;
    Rigidbody2D rb;

    float respawnTime = 10;
    float buildCoolDown = 0f;
    float shootCooldown = 0;
    float regenClock = 1;
    float invincibilityTime = 0;

    int towerID;

    bool isShooting = false;
    bool isMovingForward = false;
    bool hasDied = false;
    bool DiedTimeToRespawn;
    bool invincible;



    //Constructor
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        getColor();

        //to make battle royale last longer
        if (battleRoyale) 
        {
            hp += 20;
            maxHP += 20;
        }


        towerID = -5;

        //experimental alternate player class
        if (PlayerPrefs.GetString("isASaucer").Equals("true"))
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = saucer;
            transform.localScale = new Vector3(10, 10, 10);
            rb.mass = 0.0005f;
            rb.drag = 100;
            GetComponent<PolygonCollider2D>().isTrigger = true;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    //Sets color
    void getColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (inTeamMode)
        {
            c = new Color32((byte)255, 0, (byte)0, (byte)255);
        }
        else
        {
            //Sets all colors to what the player has chosen in settings
            c = new Color32((byte)PlayerPrefs.GetInt("red"), (byte)PlayerPrefs.GetInt("green"), (byte)PlayerPrefs.GetInt("blue"), 255);
        }
        sr.color = c;
        color = c.ToString();
    }


    //moves the player. May seem repetitive, but is necessary for cross platform controls
    void move()
    {
         if (Input.GetButton("Vertical"))
         {
             rb.AddForce(transform.up * (speed + speedUpgrade) * Input.GetAxis("Vertical"));
         }
         rb.AddTorque(Input.GetAxis("Horizontal") * turnSensitivity);
    }

    //Mobile movement
    public void buttonStartMovingForward()
    {
        isMovingForward = true;
    }

    //Mobile stop moving
    public void buttonStopMovingForward()
    {
        isMovingForward = false;
    }

    // PC Shoot
    void shoot()
    {
        if (Input.GetButton("Fire1") && shootCooldown <= 0)
        {
            Vector3 vec = transform.rotation * new Vector3(0, shotOffset, 0);
            Bullet b = shot.GetComponent<Bullet>();
            b.color = color;
            b.bulletLife = .5f + bulletLifeUpgrade;
            Debug.Log(b.bulletSpeed + bulletSpeedUpgrade * 50);
            b.bulletSpeed = 500 + (bulletSpeedUpgrade * 50);
            b.damage = 1 + bulletDamageUpgrade;
            Instantiate(shot, transform.position + vec, transform.rotation);
            shootCooldown = ShootCooldown;
        }

    }

    //JoyStick Shoot
    public void joystickShoot()
    {
        if (shootCooldown <= 0)
        {
            Vector3 vec = transform.rotation * new Vector3(0, shotOffset, 0);
            Bullet b = shot.GetComponent<Bullet>();
            b.color = color;
            b.damage = 1 + bulletDamageUpgrade;
            b.bulletLife = .5f + bulletLifeUpgrade;
            b.bulletSpeed = 500 + (bulletSpeedUpgrade * 50f);
            Instantiate(shot, transform.position + vec, transform.rotation);
            shootCooldown = ShootCooldown;
        }
    }

    //Button shoot
    public void buttonShoot()
    {
        isShooting = true;

    }

    //Stops shooting when mobile button is released
    public void buttonStopShooting()
    {
        isShooting = false;
    }

    //Checks how far the nearest tower is so that towers are not placed to close together 
    float nearestTowerDistance()
    {

        TowerCode[] t = FindObjectOfType<ArrayHolder>().towerList.ToArray();

        TowerCode closestTower = null;
        float dist = Mathf.Infinity;
        for (int h = 0; h < t.Length; h++)
        {
            float d = Vector3.Distance(this.transform.position, t[h].transform.position);
            if (closestTower == null || d < dist)
            {
                closestTower = t[h];
                dist = d;
            }
        }
        return dist;
    }

    //Places tower if conditions are met
    void placeTower()
    {
        if ((Input.GetButton("Fire3") && buildCoolDown <= 0) && nearestTowerDistance() > towerToCloseDistance)
        {
            Transform t = GetComponent<Transform>();
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            t.transform.position.Set(x, y, 0);
            TowerCode tc = tower.GetComponent<TowerCode>();
            tc.color = color;
            tc.col = c;
            tc.inTeamMode = inTeamMode;
            tc.towerID = towerID;
            tc.ZoneSize = towerZoneUpgrade;
            tc.bulletDamage = towerBulletDamageUpgrade;
            tc.hp = towerHpUpgrade + 5;
            tc.setColor();
            Instantiate(tower, this.transform.position, this.transform.rotation);
            buildCoolDown = BuildCoolDown;
        }
        else
        {
            buildCoolDown -= Time.deltaTime; //timer
        }
    }

    //Respawns player if they watch an ad
    public void respawn()
    {
        gameObject.SetActive(true);
        invincible = true;
        hp = 9999;
        invincibilityTime = 5;
        diedMenu.SetActive(false);
    }



    //places tower on mobile if conditions are met
    public void buttonPlaceTower()
    {
        if (buildCoolDown <= 0 && (nearestTowerDistance() > towerToCloseDistance))
        {
            Transform t = GetComponent<Transform>();
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            t.transform.position.Set(x, y, 0);

            TowerCode tc = tower.GetComponent<TowerCode>();
            tc.color = color;
            tc.col = c;
            tc.inTeamMode = inTeamMode;
            tc.towerID = towerID;
            tc.ZoneSize = towerZoneUpgrade;
            tc.bulletDamage = towerBulletDamageUpgrade;
            tc.hp = towerHpUpgrade + 5;
            tc.setColor();
            Instantiate(tower, this.transform.position, this.transform.rotation);
            buildCoolDown = BuildCoolDown;
        }
        else
        {
            buildCoolDown -= Time.deltaTime;
        }
    }

    //Method is called every tick of the physics engine
    void FixedUpdate()
    {
        if (invincible)
        {
            if (invincibilityTime < 0)
            {
                invincible = false;
                hp = maxHP;
            }
            else
            {
                invincibilityTime -= Time.deltaTime;
            }
        }

        if (isMovingForward)
        {
            rb.AddForce(transform.up * speed);
        }
        if (isShooting)
        {
            if (shootCooldown <= 0)
            {
                Vector3 vec = transform.rotation * new Vector3(0, shotOffset, 0);
                Bullet b = shot.GetComponent<Bullet>();
                b.damage = 1 + bulletDamageUpgrade;
                b.bulletLife = .5f + bulletLifeUpgrade;
                b.bulletSpeed = 500 + (bulletSpeedUpgrade * 50);
                b.color = color;
                Instantiate(shot, transform.position + vec, transform.rotation);
                shootCooldown = ShootCooldown;
            }
        }
        move();
        shootCooldown -= Time.deltaTime;



        if (Button.IsInvoking())
        {
            buttonShoot();
        }

        //Uncomment for PC Builds   shoot();

        placeTower();


        if (hp <= 0)
        {

            if (battleRoyale)
            {
                diedMenu.SetActive(true);
                FindObjectOfType<StartUpCode>().respawnTime = 5;
                FindObjectOfType<StartUpCode>().respawn();
            }

            if (!inTeamMode)
            {
                if (!hasDied)
                {
                    hasDied = true;
                    hp = 99999;
                    diedMenu.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Main Menu");
                    Destroy(gameObject);
                    FindObjectOfType<CamFollow>().kill();
                    FindObjectOfType<MapShipFollow>().kill();
                }
            }
            else
            {
                diedMenu.SetActive(true);
                Debug.Log("it happened");
                FindObjectOfType<StartUpCode>().respawn();
                respawnTime = 10;
            }

            gameObject.SetActive(false);

        }
        if (regenClock <= 0 && hp < maxHP)
        {
            hp++;
            regenClock = regenSpeed;
        }
        else
        {
            regenClock -= Time.deltaTime;
        }
    }

    //Takes damage if there is a collision with a different colored bullet.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() is Bullet)
        {
            if (!(collision.gameObject.GetComponent<Bullet>().color == color))
            {
                hp -= collision.gameObject.GetComponent<Bullet>().damage;
            }
        }
    }
}
