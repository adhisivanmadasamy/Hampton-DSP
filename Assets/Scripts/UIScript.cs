using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject HomePanel, LoginPanel, SignUpPanel;
    public GameObject LandingPage;

    public GameObject SV_WhatsOn, SV_Attractions;
    public GameObject B_WhatsOn, B_Attractions;
    public TextMeshProUGUI T_WhatsOn, T_Attractions;

    public GameObject SideMenuPanel;
    public string Display_Email, Display_Name;
    public TextMeshProUGUI T_DisplayName, T_DisplayEmail;

    public GameObject B_Map;
    public TextMeshProUGUI T_Map;

    // Start is called before the first frame update
    void Start()
    {
        if (helper.GetloginPAGE() == 0)
        {
            // show login panel
            HomePanel.SetActive(true);
        }
        else
        {
            // hide login panel
            HomePanel.SetActive(false);
            LoginPanel.SetActive(false);
            SignUpPanel.SetActive(false);
            LandingPage.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLoginPanel()
    {
        HomePanel.SetActive(false);
        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        HomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(true);
    }

    public void GoToLandingPage()
    {
        HomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        LandingPage.SetActive(true);
    }

    public void ClickAttraction()
    {
        SV_WhatsOn.SetActive(false);
        SV_Attractions.SetActive(true);

        B_Attractions.GetComponent<Image>().color = Color.black;
        B_Attractions.GetComponent<Image>().color = new Color(B_Attractions.GetComponent<Image>().color.r, B_Attractions.GetComponent<Image>().color.g, B_Attractions.GetComponent<Image>().color.b, 0.5f);
        B_WhatsOn.GetComponent<Image>().color = Color.white;
        B_Map.GetComponent<Image>().color = Color.white;
        T_Attractions.color = Color.black; 
        T_WhatsOn.color = Color.white;
        T_Map.color = Color.white;

    }
    public void ClickMap()
    {

        SceneManager.LoadScene("VRNavigation");

        B_Map.GetComponent<Image>().color = Color.black;
        B_Map.GetComponent<Image>().color = new Color(B_Attractions.GetComponent<Image>().color.r, B_Attractions.GetComponent<Image>().color.g, B_Attractions.GetComponent<Image>().color.b, 0.5f);
        B_Attractions.GetComponent<Image>().color= Color.white;
        B_WhatsOn.GetComponent<Image>().color = Color.white;
        T_Attractions.color = Color.white;
        T_WhatsOn.color = Color.white;
        T_Map.color = Color.black;

    }

    public void ClickWhatsOn()
    {
        SV_WhatsOn.SetActive(true);
        SV_Attractions.SetActive(false);

        B_Attractions.GetComponent<Image>().color = Color.white;
        B_WhatsOn.GetComponent<Image>().color = Color.black;
        B_WhatsOn.GetComponent<Image>().color = new Color(B_WhatsOn.GetComponent<Image>().color.r, B_WhatsOn.GetComponent<Image>().color.g, B_WhatsOn.GetComponent<Image>().color.b, 0.5f);
        B_Map.GetComponent<Image>().color = Color.white;
        T_Attractions.color = Color.white;
        T_WhatsOn.color = Color.black;
        T_Map.color = Color.white;

    }

    public void ClickSideMenu()
    {
        SideMenuPanel.SetActive(true);

    }

    public void CloseSideMenu()
    {
        SideMenuPanel.SetActive(false);
    }

    public void AuthSuccess()
    {
        HomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        LandingPage.SetActive(true);
        T_DisplayEmail.text = Display_Email;
        T_DisplayName.text = Display_Name;
    }



}
