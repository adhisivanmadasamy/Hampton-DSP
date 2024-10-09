using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper : MonoBehaviour
{
    public static int GetloginPAGE() // 
    {
        return PlayerPrefs.GetInt("Loginpage");
    }
    public static void setloginpage(int i) // i==0 login page will appear || i==1 login page hide
    {
        PlayerPrefs.SetInt("Loginpage", i);
    }
}
