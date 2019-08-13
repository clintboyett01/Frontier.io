using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    Controller Controller;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Controller = FindObjectOfType<Controller>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = Controller.maxHP;
        slider.value = Controller.hp;
    }
}
