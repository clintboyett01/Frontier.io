using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCode : MonoBehaviour
{
    public GameObject shot;
    public GameObject zone;
    public int hp = 5;
    public float turnSpeed = 5f;
    public float shootRange = 20;
    public float shootCooldown = 0.5f;
    float cooldownLeft = 0;
    public string color;
    public int towerID;
    public float shotOffset;
    public Color col;
    float targetDistance;
    int myNum;
    public bool isTower = true;
    public int bulletDamage;
    public float ZoneSize;
    public float timeMultiplier;
    float timeTillAlive = 1.5f;
    public float age = 20;
    public bool inTeamMode;


    //kills itself if it's player dies
    public void teamDie(int deadTeamID)
    {
        if (towerID == deadTeamID)
        {
            Destroy(gameObject);
            Object[] g = FindObjectsOfType(typeof(Zone));
            for (int h = 0; h < g.Length; h++)
            {
                Zone temp = (Zone)g[h];
                temp.tow(myNum);
            }
        }
    }


    //sets its color
    public void setColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = col;
    }


  
    private void Start()
    {
        
        getMyNum();
        Zone z = zone.GetComponent<Zone>();
        z.col = col;
        z.size = ZoneSize;
        z.num = myNum;
        z.setColor();
        Instantiate(zone, this.transform.position, this.transform.rotation);

    }

    void getMyNum()
    {

        myNum = StartUpCode.towNum;
        StartUpCode.towNum++;
    }

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

    void pointTo(Target nextTarget)
    {
        Vector2 v = nextTarget.transform.position - this.transform.position;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        targetDistance = v.magnitude;
    }

    void shoot()
    {
        if (cooldownLeft <= 0 && targetDistance < shootRange*ZoneSize)
        {
            Vector3 vec = transform.rotation * new Vector3(0, shotOffset, 0);
            Bullet b = shot.GetComponent<Bullet>();
            b.color = color;
            b.bulletLife = timeMultiplier * ZoneSize; //0.38f - (10/Mathf.Pow(2,ZoneSize));
            b.damage = 1 + (bulletDamage / 2);
            b.bulletSpeed = 500;
            Instantiate(shot, transform.position + vec, transform.rotation);
            cooldownLeft = shootCooldown;
        }
        else
        {
            cooldownLeft -= Time.deltaTime;
        }
    }

    void checkForDeath()
    {
        if (hp <= 0)
        {
            FindObjectOfType<ArrayHolder>().towerList.Remove(this);
            Destroy(gameObject);
            Object[] g = FindObjectsOfType(typeof(Zone));
            for (int h = 0; h < g.Length; h++)
            {
                Zone temp = (Zone)g[h];
                temp.tow(myNum);
            }

        }
    }

    void FixedUpdate()
    {
        if (inTeamMode)
        {
            if(age < 0)
            {
                hp = 0;
            }
            else
            {
                age -= Time.deltaTime;
            }
        }
        Target nextTarget = getNextTarget();

        if (nextTarget != null)
        {
            pointTo(nextTarget);
            shoot();
        }

        if (timeTillAlive < 0)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            timeTillAlive -= Time.deltaTime;
        }
        checkForDeath();
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
        catch (System.Exception e)
        {
        }
    }

}
