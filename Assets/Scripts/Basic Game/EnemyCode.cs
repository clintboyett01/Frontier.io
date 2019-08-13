using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCode : MonoBehaviour
{
    public GameObject tower;
    public GameObject shot;
    public GameObject homePoint;
    public float turnSpeed = 2;
    public float shootCooldown = 3;
    private float cooldownLeft = 0;
    public float shootRange = 10;
    public float toCloseRange = 2;
    public float safeZone = 200;
    public float speed;
    public string color;
    public float shotOffset;
    public int score;
    public int scoreUsed;
    public int scoreToSpend;
    public int maxHp = 10;
    public int hp = 10;
    public float regenSpeed;
    int speedUpgrade;
    public float towerPlaceCooldown = 10;
    float towerCooldown = 0;
    Color32 c;
    TowerCode[] myTeam;
    public float towerToCloseDistance;
    public float agressiveness = .5f;
    int towersPlaced;
    public bool isTower = true;
    public float romingDist;
    Rigidbody2D rb;
    //  public team team;
    //  ScoreBoard scoreBoard;
    float regenClock;
    ScoreTracker st;

    public int UpgradesCost = 40;
    public float hpRegen = 0;
    public int umaxHp = 0;
    public int bulletFireRate = 0;
    public int bulletDamage = 0;
    public int bulletSpeed = 0;
    public int bulletLife = 0;
    public int towerHp = 0;
    public int towerBulletDamage = 0;
    public int towerBuildRate = 0;
    public float towerZone = 0;

    public int scoreGivenToTeam = 0;
    public bool inTeamMode;
    public Color teamModeColor;
    public bool battleRoyale;
    public int towerID;

    void Start()
    {
    //    HomePoint home = homePoint.GetComponent<HomePoint>();
    //    home.color = color;
    //    Instantiate(home, this.transform.position, this.transform.rotation);
        setColor();
        rb = GetComponent<Rigidbody2D>();
        st = GetComponent<ScoreTracker>();
        if (battleRoyale)
        {
            hp += 20;
            maxHp += 20;
        }
        towerID = Random.Range(0, 999999);
  //      scoreBoard = FindObjectOfType<ScoreBoard>();
  //      team = new team();
  //      team.color = color;
    }

    void setColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (inTeamMode)
        {
            c = teamModeColor;
        }
        else
        {
            c = StartUpCode.colors.Dequeue();
        }
        
        sr.color = c;
        color = c.ToString();
    }

    //Color32 getRandomColor()
    //{
    //    Color32 temp = new Color32();
    //    int randNum = (Random.Range(0, 255) / 50) * 50;
    //    switch (Random.Range(1, 6))
    //    {
    //        case 1: temp.r = (byte)randNum; temp.b = 0xFF; temp.g = 0x0; temp.a = 0xFF; break;
    //        case 2: temp.r = 0x0; temp.b = (byte)randNum; temp.g = 0xFF; temp.a = 0xFF; break;
    //        case 3: temp.r = 0xFF; temp.b = 0x0; temp.g = (byte)randNum; temp.a = 0xFF; break;
    //        case 4: temp.r = (byte)randNum; temp.b = 0x0; temp.g = 0xFF; temp.a = 0xFF; break;
    //        case 5: temp.r = 0xFF; temp.b = (byte)randNum; temp.g = 0x0; temp.a = 0xFF; break;
    //        case 6: temp.r = 0x0; temp.b = 0xFF; temp.g = (byte)randNum; temp.a = 0xFF; break;
    //    }
    //    Object[] g = FindObjectsOfType(typeof(EnemyCode));
    //    Object[] g2 = FindObjectsOfType(typeof(Controller));
    //    for (int c = 0; c < g.Length; c++)
    //    {
    //        EnemyCode e = (EnemyCode)g[c];
    //        if (temp.ToString().Equals(e.color))
    //        {
    //            return getRandomColor();
    //        }
    //    }
    //    for (int c = 0; c < g2.Length; c++)
    //    {
    //        Controller e = (Controller)g2[c];
    //        if (temp.ToString().Equals(e.color))
    //        {
    //            return getRandomColor();
    //        }
    //    }
    //    return temp;
    //}
    Target[] GetTarget()
    {
        Object[] g = FindObjectsOfType(typeof(Target));
        Target[] t = new Target[g.Length];

        for (int h = 0; h < g.Length; h++)
        {
            Target temp = (Target)g[h];
            if (!temp.color.Equals(this.color))
            {
                t[h] = temp;
            }
            else
            {
                t[h] = null;
            }
        }
        return t;
    }



    Target getNextTarget()
    {
        Target[] t = GetTarget();
        Target nextTarget = null;
        float dist = Mathf.Infinity;
        for (int h = 0; h < t.Length; h++)
        {
            if (t[h] != null)
            {
                float d = Vector3.Distance(this.transform.position, t[h].transform.position);

                if (nextTarget == null || d < dist)
                {
                    nextTarget = t[h];
                    dist = d;
                }
            }
        }
        return nextTarget;
    }

    void Move(Target nextTarget)
    {
        if (nextTarget==null)
        {
            getNextTarget();
        }
        else if (hp < maxHp/4&& Vector2.Distance(transform.position, nextTarget.transform.position)<safeZone)
        {
            Vector2 v = nextTarget.transform.position - this.transform.position;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle +90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            rb.AddForce(transform.up * -(speed + speedUpgrade));
            //transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, -speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, nextTarget.transform.position) > shootRange /*&& Vector2.Distance(transform.position, homePoint.transform.position) < agressiveness*(towersPlaced)*romingDist*/)
        {
            Vector2 v = nextTarget.transform.position - this.transform.position;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, speed * Time.deltaTime);
            rb.AddForce(transform.up * ((speed + speedUpgrade)));
        }
        else if (Vector2.Distance(transform.position, nextTarget.transform.position) < toCloseRange )
        {
            Vector2 v = nextTarget.transform.position - this.transform.position;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, -speed * Time.deltaTime);
            rb.AddForce(transform.up * (-(speed + speedUpgrade)));
            shoot();
        }
        else if (Vector2.Distance(transform.position, nextTarget.transform.position) < shootRange /*&& Vector2.Distance(transform.position, homePoint.transform.position) < agressiveness*(towersPlaced)*romingDist*/)
        {
            Vector2 v = nextTarget.transform.position - this.transform.position;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            //rb.AddForce(transform.up * (speed));
            shoot();
        }
        
    }

    void shoot()
    {
        if (cooldownLeft <= 0)
        {
            Vector3 vec = transform.rotation * new Vector3(0, shotOffset, 0);
            Bullet b = shot.GetComponent<Bullet>();
            b.color = color;
            b.damage = bulletDamage + 1;
            b.bulletLife = .5f + bulletLife;
            b.bulletSpeed = 500 + (bulletSpeed * 50f); 
            Instantiate(shot, transform.position + vec, transform.rotation);
            cooldownLeft = shootCooldown;
        }
        
        
    }

    float nearestTowerDistance()
    {

        Object[] g = FindObjectsOfType(typeof(TowerCode));
        TowerCode[] t = new TowerCode[g.Length];
        for (int h = 0; h < g.Length; h++)
        {
            TowerCode temp = (TowerCode)g[h];
            t[h] = temp;
        }
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
    TowerCode nearestTower()
    {

        Object[] g = FindObjectsOfType(typeof(TowerCode));
        TowerCode[] t = new TowerCode[g.Length];
        for (int h = 0; h < g.Length; h++)
        {
            TowerCode temp = (TowerCode)g[h];
            if (temp.color.Equals(this.color))
            {
                t[h] = temp;
            }
            else
            {
                t[h] = null;
            }
        }
        TowerCode closestTower = null;
        float dist = Mathf.Infinity;
        for (int h = 0; h < t.Length; h++)
        {
            try
            {
                float d = Vector3.Distance(this.transform.position, t[h].transform.position);

                if (closestTower == null || d < dist)
                {
                    closestTower = t[h];
                    dist = d;
                }
            }
            catch { }
        }
        return closestTower;
    }



    void buildTower()
    {
        if (towerCooldown <= 0&&nearestTowerDistance() > towerToCloseDistance)
        {
            Transform tra = GetComponent<Transform>();
            float x = transform.position.x;
            float y = transform.position.y;
            tra.transform.position.Set(x, y, 0);
            towersPlaced++;
            TowerCode tc = tower.GetComponent<TowerCode>();
            tc.color = color;
            tc.col = c;
            tc.inTeamMode = inTeamMode;
            tc.towerID = towerID;
            tc.ZoneSize = towerZone + 1;
            tc.setColor();
            tc.bulletDamage = towerBulletDamage;
            tc.hp = towerHp + 5;
            Instantiate(tower, this.transform.position, this.transform.rotation);
            towerCooldown = towerPlaceCooldown;
        }
        else
        {
            towerCooldown -= Time.deltaTime;
        }
    }

    void checkForDeath()
    {
        if (hp <= 0)
        {
            
            //FindObjectOfType<StartUpCode>().addName(GetComponent<ScoreTracker>().name);
            //FindObjectOfType<StartUpCode>().addColor(color);
            FindObjectOfType<ArrayHolder>().scoreTracker.Remove(GetComponent<ScoreTracker>());
            Destroy(gameObject);
            if (inTeamMode)
            {

                FindObjectOfType<StartUpCode>().summonEnemyTeamMode(c);
            }
            else
            {
                killTeam();
            }
        }
    }

    void killTeam()
    {
        Object[] g = FindObjectsOfType(typeof(TowerCode));
        for (int h = 0; h < g.Length; h++)
        {
            TowerCode temp = (TowerCode)g[h];
            temp.teamDie(towerID);
        }
   //     team.teamDie();
    }

    void FixedUpdate()
    {
        Target nextTarget = getNextTarget();
        score = st.score;
        scoreToSpend += score - scoreUsed;
        scoreUsed = score;
        if (!(nextTarget == null))
        {
            Move(nextTarget);
            
        }
        if (regenClock <= 0 && hp < maxHp)
        {
            hp++;
            regenClock = regenSpeed;
        }
        else
        regenClock -= Time.deltaTime;
        cooldownLeft -= Time.deltaTime;
        buildTower();
        checkForDeath();
        if(scoreToSpend > UpgradesCost)
        {
            scoreToSpend -= UpgradesCost;
            UpgradesCost += 5;
            if (!inTeamMode)
            {
                doRandomUpgrade();
            }
        }
        if (inTeamMode)
        {
            int tempScore = score - scoreGivenToTeam;
            scoreGivenToTeam = score;
            if (c.b == 255)
            {
                StartUpCode.blueTeamScore += tempScore;
            }
            if (c.r == 255)
            {
                StartUpCode.redTeamScore += tempScore;
            }
        }
    }

    void doRandomUpgrade()
    {
        switch (Random.Range(1, 12))
        {
            case 1:  upgradeSpeed(); break;
            case 2:  upgradeHealthRegen(); break;
            case 3:  upgradeHealthMax(); break;
            case 4:  upgradeBulletFireRate(); break;
            case 5:  upgradeBulletDamage(); break;
            case 6:  upgradeBulletSpeed(); break;
            case 7:  upgradeBulletLife(); break;
            case 8:  upgradeTowerHealth(); break;
            case 9:  upgradeTowerBulletDamage(); break;
            case 10: upgradeTowerBuildRate(); break;
            case 11: upgradeTowerZone(); break;
        }
    }


    public void upgradeSpeed()
    {
        if (speedUpgrade < 10)
        {
            speedUpgrade++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeHealthRegen()
    {
        if (hpRegen < 10)
        {
            hpRegen++;
            regenSpeed -= (hpRegen / 15f);
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeHealthMax()
    {
        if (umaxHp < 10)
        {
            umaxHp++;
            maxHp += umaxHp;
            
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }



    public void upgradeBulletFireRate()
    {
        if (bulletFireRate < 10)
        {
            bulletFireRate++;
            shootCooldown -= .04f;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeBulletDamage()
    {
        if (bulletDamage < 10)
        {
            bulletDamage++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeBulletSpeed()
    {
        if (bulletSpeed < 10)
        {
            bulletSpeed++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeBulletLife()
    {
        if (bulletLife < 10)
        {
            bulletLife++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeTowerHealth()
    {
        if (towerHp < 10)
        {
            towerHp++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }
    public void upgradeTowerBulletDamage()
    {
        if (towerBulletDamage < 10)
        {
            towerBulletDamage++;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    public void upgradeTowerBuildRate()
    {
        if (towerBuildRate < 10)
        {
            towerBuildRate++;
            towerCooldown -= 0.5f;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }

    }

    public void upgradeTowerZone()
    {
        if (towerZone < 2.5)
        {
            towerZone += .25f;
        }
        else
        {
            scoreToSpend += UpgradesCost;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (!(collision.gameObject.GetComponent<Bullet>().color == color))
            {
                hp -= collision.gameObject.GetComponent<Bullet>().damage;
            }
        }
        catch (System.Exception e) {
        }
    }
}
