using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 500f;
    public int damage = 1;
    public float bulletLife = 3;
    public string color;


    
    void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
//        rb.AddForce(transform.up * bulletSpeed / ((3-bulletLife)/2));
        bulletLifeTracker();
    }

    void bulletLifeTracker()
    {
        if (bulletLife <= 0)
        {
            Destroy(gameObject);
        }
        bulletLife -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        try
        {
            if (!(collision.gameObject.GetComponent<Target>().color == color))
            {
                
                foreach(ScoreTracker st in FindObjectOfType<ArrayHolder>().scoreTracker)
                {
                    if (st.color.Equals(this.color))
                    {
                        st.addScore();
                        break;
                    }
                }
                Destroy(gameObject);

            }
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }

    }

}
