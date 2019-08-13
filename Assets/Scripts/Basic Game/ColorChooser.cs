using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChooser : MonoBehaviour
{
    public Slider red;
    public Slider blue;
    public Slider green;

    Image image;
    Color32 newColor;
    
    void Start()
    {
        image = GetComponent<Image>();
        newColor = new Color32();
    }
    
    void Update()
    {
        newColor.a = 255;
        newColor.r = (byte)red.value;
        newColor.b = (byte)blue.value;
        newColor.g = (byte)green.value;
        image.color = newColor;
    }
}
