using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntersceneController : MonoBehaviour
{
    private const int MaxPlayers = 4;

    private int PlayerCount;
    private int[] Score;
    
    private string[] Names;

    private bool Wild;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] ControllerInstances = GameObject.FindGameObjectsWithTag("Interscene");
        if (ControllerInstances.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        PlayerCount = 1;
        Score = new int[MaxPlayers];
        Names = new string[] { "Player 1", "Player 2", "Player 3", "Player 4" };
        Wild = false;
    }

    public void SetPlayerCount(string Mode)
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
    }

    public void SetName(string PlayerName, string PlayerNum)
    {
        switch (PlayerNum)
        {
            case "Name1":
                Names[0] = PlayerName;
                break;
            case "Name2":
                Names[1] = PlayerName;
                break;
            case "Name3":
                Names[2] = PlayerName;
                break;
            case "Name4":
                Names[3] = PlayerName;
                break;
        }
    }

    public void EndGame(int[] Scores)
    {
        Array.Copy(Scores, Score, MaxPlayers);
        SceneManager.LoadScene("Results", LoadSceneMode.Single);
    }

    public void ToggleWild()
    {
        if (Wild)
        {
            Wild = false;
        }
        else
        {
            Wild = true;
        }
    }

    public void ResetValues()
    {
        Names[0] = "Player 1";
        Names[1] = "Player 2";
        Names[2] = "Player 3";
        Names[3] = "Player 4";
        Wild = false;
    }

    public bool WildStatus()
    {
        return Wild;
    }

    public int[] GetScores()
    {
        return Score;
    }

    public string[] GetNames()
    {
        return Names;
    }

    public int GetPlayerCount()
    {
        return PlayerCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
