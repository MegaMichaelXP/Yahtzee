using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    private const int MaxPlayers = 4;

    private IntersceneController ISC;

    //private GameObject[] Scoreboard;

    private GameObject Scoreboard1;
    private GameObject Scoreboard2;
    private GameObject Scoreboard3;
    private GameObject Scoreboard4;

    private GameObject Crown1;
    private GameObject Crown2;
    private GameObject Crown3;
    private GameObject Crown4;

    //private GameObject[] Crowns;

    private UnityEngine.UI.Text[] ScoreboardText;

    private int[] Score;
    private int[] PlayerIndexes;
    private int PlayerCount;

    private int Tie1Start;
    private int Tie1End;
    private int Tie2Start;
    private int Tie2End;

    private bool FoundTie;

    private string[] Names;

    // Start is called before the first frame update
    void Start()
    {
        Score = new int[MaxPlayers];
        Names = new string[MaxPlayers];
        Tie1Start = -1;
        Tie1End = -1;
        Tie2Start = -1;
        Tie2End = -1;
        FoundTie = false;
        PlayerIndexes = new int[] { 0, 1, 2, 3 };
        //Crowns = GameObject.FindGameObjectsWithTag("Crown");
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        //Scoreboard = GameObject.FindGameObjectsWithTag("Score");
        Scoreboard1 = GameObject.Find("Score1");
        Scoreboard2 = GameObject.Find("Score2");
        Scoreboard3 = GameObject.Find("Score3");
        Scoreboard4 = GameObject.Find("Score4");
        Crown1 = GameObject.Find("Crown1");
        Crown2 = GameObject.Find("Crown2");
        Crown3 = GameObject.Find("Crown3");
        Crown4 = GameObject.Find("Crown4");
        ScoreboardText = new UnityEngine.UI.Text[MaxPlayers];
        ScoreboardText[0] = Scoreboard1.GetComponent<UnityEngine.UI.Text>();
        ScoreboardText[1] = Scoreboard2.GetComponent<UnityEngine.UI.Text>();
        ScoreboardText[2] = Scoreboard3.GetComponent<UnityEngine.UI.Text>();
        ScoreboardText[3] = Scoreboard4.GetComponent<UnityEngine.UI.Text>();
        PlayerCount = ISC.GetPlayerCount();
        if (PlayerCount > 1)
        {
            Scoreboard2.SetActive(true);
        }
        else
        {
            Scoreboard2.SetActive(false);
        }
        if (PlayerCount > 2)
        {
            Scoreboard3.SetActive(true);
        }
        else
        {
            Scoreboard3.SetActive(false);
        }
        if (PlayerCount > 3)
        {
            Scoreboard4.SetActive(true);
        }
        else
        {
            Scoreboard4.SetActive(false);
        }
        if (PlayerCount > 1)
        {
            Crown1.SetActive(true);
        }
        else
        {
            Crown1.SetActive(false);
        }
        GetData();
        for (int i = 0; i < MaxPlayers; i++)
        {
            ScoreboardText[i].text = Names[i] + ": " + Score[i].ToString();
        }
        if (Score[1] == Score[0] && PlayerCount > 1)
        {
            Crown2.SetActive(true);
        }
        else
        {
            Crown2.SetActive(false);
        }
        if (Score[2] == Score[0] && PlayerCount > 2)
        {
            Crown3.SetActive(true);
        }
        else
        {
            Crown3.SetActive(false);
        }
        if (Score[3] == Score[0] && PlayerCount > 3)
        {
            Crown4.SetActive(true);
        }
        else
        {
            Crown4.SetActive(false);
        }
    }

    void GetData()
    {
        Array.Copy(ISC.GetScores(), Score, MaxPlayers);
        Array.Copy(ISC.GetNames(), Names, MaxPlayers);
        PlayerCount = ISC.GetPlayerCount();
        int TempScore;
        int TempIndex;
        string TempName;
        for (int i = 0; i < MaxPlayers - 1; i++)
        {
            for (int j = 0; j < MaxPlayers - i - 1; j++)
            {
                if (Score[j] <= Score[j + 1])
                {
                    TempScore = Score[j];
                    TempIndex = PlayerIndexes[j];
                    TempName = Names[j];
                    Score[j] = Score[j + 1];
                    PlayerIndexes[j] = PlayerIndexes[j + 1];
                    Names[j] = Names[j + 1];
                    Score[j + 1] = TempScore;
                    PlayerIndexes[j + 1] = TempIndex;
                    Names[j + 1] = TempName;
                }
            }
        }
        for (int i = 0; i < MaxPlayers - 1; i++)
        {
            if (Score[i] == Score[i + 1])
            {
                if (!FoundTie)
                {
                    Tie1End = i + 1;
                    if (Tie1Start == -1)
                    {
                        Tie1Start = i;
                    }
                }
                else
                {
                    Tie2End = i + 1;
                    if (Tie2Start == -1)
                    {
                        Tie2Start = i;
                    }
                }
            }
            else
            {
                if (Tie1End != -1)
                {
                    FoundTie = true;
                }
            }
        }
        if (Tie1Start != -1)
        {
            CleanScoreboard(Tie1Start, Tie1End);
        }
        if (Tie2Start != -1)
        {
            if (PlayerIndexes[Tie2Start] > PlayerIndexes[Tie2End])
            {
                TempScore = Score[Tie2Start];
                TempIndex = PlayerIndexes[Tie2Start];
                TempName = Names[Tie2Start];
                Score[Tie2Start] = Score[Tie2End];
                PlayerIndexes[Tie2Start] = PlayerIndexes[Tie2End];
                Names[Tie2Start] = Names[Tie2End];
                Score[Tie2End] = TempScore;
                PlayerIndexes[Tie2End] = TempIndex;
                Names[Tie2End] = TempName;
            }
        }
    }

    void CleanScoreboard(int Start, int End)
    {
        int TempScore;
        int TempIndex;
        string TempName;
        for (int i = Start; i < End; i++)
        {
            for (int j = Start; j < End - i; j++)
            {
                if (PlayerIndexes[j] > PlayerIndexes[j + 1])
                {
                    TempScore = Score[j];
                    TempIndex = PlayerIndexes[j];
                    TempName = Names[j];
                    Score[j] = Score[j + 1];
                    PlayerIndexes[j] = PlayerIndexes[j + 1];
                    Names[j] = Names[j + 1];
                    Score[j + 1] = TempScore;
                    PlayerIndexes[j + 1] = TempIndex;
                    Names[j + 1] = TempName;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
