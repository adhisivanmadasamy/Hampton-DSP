using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScriptVR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickARNav()
    {
        SceneManager.LoadScene("ARNavigation");
    }

    public void ClickHome()
    {
        helper.setloginpage(1);
        SceneManager.LoadScene("Home");
    }
}
