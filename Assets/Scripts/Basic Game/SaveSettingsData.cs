using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettingsData : MonoBehaviour
{
    public int red;
    public int blue;
    public int green;
    public string name;
    public bool joystickFixed;
    public bool altranateControls;

    public Slider redS;
    public Slider blueS;
    public Slider greenS;
    public Text nameT;
    public Toggle jsf;
    public Toggle altc;


  

    private void Start()
    {
        if (!PlayerPrefs.HasKey("NotFirstTimePlaying"))
        {
            red = 0;
            blue = 255;
            green = 255;
            name = "Anonymous";
            joystickFixed = false;
            altranateControls = false;
            setColors(red, blue, green);
            setName(name);
            setControls(joystickFixed, altranateControls);
            PlayerPrefs.SetInt("NotFirstTimePlaying",0);
            save();
        }
    }

    public void save()
    {
        PlayerPrefs.Save();
    }
    public void setColors(int red, int blue, int green)
    {
        PlayerPrefs.SetInt("red", red);
        PlayerPrefs.SetInt("blue", blue);
        PlayerPrefs.SetInt("green", green);
    }
    public void setName(string name)
    {
        PlayerPrefs.SetString("name", name);
    }
    public void setControls(bool joystickFixed, bool altranateControls)
    {
        int jf;
        if (joystickFixed)
        {
            jf = 1;
        }
        else
        {
            jf = 0;
        }
        int ac;
        if (altranateControls)
        {
            ac = 1;
        }
        else
        {
            ac = 0;
        }
        PlayerPrefs.SetInt("joystickFixed", jf);
        PlayerPrefs.SetInt("altranateControls", ac);
    }

    public void colorSave()
    {
        red = (int)redS.value;
        blue = (int)blueS.value;
        green = (int)greenS.value;
        setColors(red, blue, green);
        PlayerPrefs.Save();
    }

    public void nameSave()
    {
        name = nameT.text;
        setName(name);
        PlayerPrefs.Save();
    }

    public void controlsSave()
    {
        joystickFixed = jsf.isOn;
        altranateControls = altc.isOn;
        setControls(joystickFixed, altranateControls);
        PlayerPrefs.Save();
    }

}
