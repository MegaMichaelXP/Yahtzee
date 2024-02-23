using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private GameObject Main;
    private GameObject Names;
    private GameObject HowToPlay;

    private GameObject GuidePage1;
    private GameObject GuidePage2;
    private GameObject GuidePage3;
    private GameObject GuidePage4;

    private GameObject PreviousButton;
    private GameObject NextButton;

    private GameObject Name1;
    private GameObject Name2;
    private GameObject Name3;
    private GameObject Name4;

    IntersceneController ISC;

    private int PlayerCount;
    private int GuidePage;

    // Start is called before the first frame update
    void Start()
    {
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        Main = GameObject.Find("Main");
        Names = GameObject.Find("Names");
        HowToPlay = GameObject.Find("HowToPlay");
        GuidePage1 = GameObject.Find("Page1");
        GuidePage2 = GameObject.Find("Page2");
        GuidePage3 = GameObject.Find("Page3");
        GuidePage4 = GameObject.Find("Page4");
        PreviousButton = GameObject.Find("PreviousButton");
        NextButton = GameObject.Find("NextButton");
        Name1 = GameObject.Find("Name1");
        Name2 = GameObject.Find("Name2");
        Name3 = GameObject.Find("Name3");
        Name4 = GameObject.Find("Name4");
        PlayerCount = 1;
        GuidePage = 1;
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        Names.SetActive(false);
        HowToPlay.SetActive(false);
        Main.SetActive(true);
    }

    public void ShowGuide()
    {
        Names.SetActive(false);
        HowToPlay.SetActive(true);
        Main.SetActive(false);
        PreviousButton.SetActive(false);
        NextButton.SetActive(true);
        GuidePage1.SetActive(true);
        GuidePage2.SetActive(false);
        GuidePage3.SetActive(false);
        GuidePage4.SetActive(false);
        GuidePage = 1;
    }

    public void NextGuidePage()
    {
        if (GuidePage < 4)
        {
            GuidePage++;
            if (GuidePage == 2)
            {
                GuidePage1.SetActive(false);
                GuidePage2.SetActive(true);
                GuidePage3.SetActive(false);
                GuidePage4.SetActive(false);
            }
            else if (GuidePage == 3)
            {
                GuidePage1.SetActive(false);
                GuidePage2.SetActive(false);
                GuidePage3.SetActive(true);
                GuidePage4.SetActive(false);
            }
            else if (GuidePage == 4)
            {
                GuidePage1.SetActive(false);
                GuidePage2.SetActive(false);
                GuidePage3.SetActive(false);
                GuidePage4.SetActive(true);
                NextButton.SetActive(false);
            }
            PreviousButton.SetActive(true);
        }
    }

    public void PrevGuidePage()
    {
        if (GuidePage > 1)
        {
            GuidePage--;
            if (GuidePage == 1)
            {
                GuidePage1.SetActive(true);
                GuidePage2.SetActive(false);
                GuidePage3.SetActive(false);
                GuidePage4.SetActive(false);
                PreviousButton.SetActive(false);
            }
            else if (GuidePage == 2)
            {
                GuidePage1.SetActive(false);
                GuidePage2.SetActive(true);
                GuidePage3.SetActive(false);
                GuidePage4.SetActive(false);
            }
            else if (GuidePage == 3)
            {
                GuidePage1.SetActive(false);
                GuidePage2.SetActive(false);
                GuidePage3.SetActive(true);
                GuidePage4.SetActive(false);
            }
            NextButton.SetActive(true);
        }
    }

    public void PreGame(string Mode)
    {
        switch (Mode)
        {
            case "1Player":
                PlayerCount = 1;
                break;
            case "2Players":
                PlayerCount = 2;
                break;
            case "3Players":
                PlayerCount = 3;
                break;
            case "4Players":
                PlayerCount = 4;
                break;
        }
        Main.SetActive(false);
        Names.SetActive(true);
        if (PlayerCount < 2)
        {
            Name2.SetActive(false);
        }
        else
        {
            Name2.SetActive(true);
        }
        if (PlayerCount < 3)
        {
            Name3.SetActive(false);
        }
        else
        {
            Name3.SetActive(true);
        }
        if (PlayerCount < 4)
        {
            Name4.SetActive(false);
        }
        else
        {
            Name4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
