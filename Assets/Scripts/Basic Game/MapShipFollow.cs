using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShipFollow : MonoBehaviour
{

    public GameObject target;
    public Color col;

  //  void Start()
    
    //{
  //      col = target.GetComponent<Controller>().MyColor;
   //     SpriteRenderer sr = GetComponent<SpriteRenderer>();
      //  sr.color = col;
  //  }

    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 0);
        transform.rotation = target.transform.rotation;

    }
    public void kill()
    {
        Destroy(this);
    }

}

