using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private const int PossibleDiceValues = 6;
    private const int NumberOfDice = 5;

    private const int SilverBonus = 35;
    private const int GoldBonus = 100;

    private const int CategoryCount = 13;
    private const int MaxPlayers = 4;
    private const int TurnLimit = 13;

    private const int FullHouseScore = 25;
    private const int SmallStraightScore = 30;
    private const int LargeStraightScore = 40;
    private const int YahtzeeScore = 50;

    private const int MinStraight = 4;

    private const int BonusThreshold = 63;

    private GameObject[] CategoryButtonObjects;
    private CategoryButton[] CategoryButtons;

    private GameObject SilverStar;
    private GameObject GoldStar;
    private GameObject BonusObject;

    private IntersceneController ISC;

    private KeyReceptor Receptor;

    private UnityEngine.UI.Text Scoreboard;
    private UnityEngine.UI.Text PlayerUp;
    private UnityEngine.UI.Text StatusIndicator;
    private UnityEngine.UI.Text BonusCounter;

    private int[] Score;
    private int[] UpperScore;
    private int[] DiceCount;
    private int[] DiceValues;
    private int[] GoldStars;
    private int[,] CategoryStates;
    private int HighestDiceCount;
    private int Sequence;
    private int PlayerTurn;
    private int PlayerCount;
    private int TurnNumber;
    private int FirstPlayer;

    private string[] Names;
    private string SelectedCategory;

    private bool[] SilverStars;
    private bool Three;
    private bool Four;
    private bool Five;
    private bool FullHouse;
    private bool SmallStraight;
    private bool LargeStraight;
    private bool Joker;

    // Start is called before the first frame update
    void Start()
    {
        SelectedCategory = "";
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        Receptor = GameObject.Find("KeyReceptor").GetComponent<KeyReceptor>();
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        Names = new string[MaxPlayers];
        Array.Copy(ISC.GetNames(), Names, MaxPlayers);
        CategoryButtonObjects = new GameObject[13];
        Joker = false;
        //CategoryButtonObjects = GameObject.FindGameObjectsWithTag("Category");
        CategoryButtons = new CategoryButton[CategoryCount];
        SilverStar = GameObject.Find("SilverStar");
        GoldStar = GameObject.Find("GoldStar");
        CategoryButtonInit();
        for (int i = 0; i < CategoryCount; i++)
        {
            CategoryButtons[i] = CategoryButtonObjects[i].GetComponent<CategoryButton>();
        }
        DiceCount = new int[] { 5, 0, 0, 0, 0, 0 };
        DiceValues = new int[] { 1, 1, 1, 1, 1 };
        Score = new int[] { 0, 0, 0, 0 };
        UpperScore = new int[] { 0, 0, 0, 0 };
        GoldStars = new int[] { 0, 0, 0, 0 };
        SilverStars = new bool[] { false, false, false, false };
        CategoryStates = new int[CategoryCount, MaxPlayers];
        for (int i = 0; i < CategoryCount; i++)
        {
            for (int j = 0; j < MaxPlayers; j++)
            {
                CategoryStates[i, j] = 0;
            }
        }
        Sequence = 0;
        PlayerCount = ISC.GetPlayerCount();
        TurnNumber = 1;
        FirstPlayer = UnityEngine.Random.Range(0, PlayerCount);
        PlayerTurn = FirstPlayer;
        Scoreboard = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        Scoreboard.text = "Score: " + Score[PlayerTurn].ToString() + " (" + UpperScore[PlayerTurn].ToString() + ")";
        PlayerUp = GameObject.Find("Player").GetComponent<UnityEngine.UI.Text>();
        PlayerUp.text = Names[PlayerTurn]; PlayerUp.text = Names[PlayerTurn];
        StatusIndicator = GameObject.Find("Status").GetComponent<UnityEngine.UI.Text>();;
        BonusCounter = GameObject.Find("BonusCount").GetComponent<UnityEngine.UI.Text>();
        BonusCounter.text = "X " + GoldStars[PlayerTurn].ToString();
        SilverStar.SetActive(false);
        GoldStar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CategoryButtonInit()
    {
        CategoryButtonObjects[0] = GameObject.Find("1s");
        CategoryButtonObjects[1] = GameObject.Find("2s");
        CategoryButtonObjects[2] = GameObject.Find("3s");
        CategoryButtonObjects[3] = GameObject.Find("4s");
        CategoryButtonObjects[4] = GameObject.Find("5s");
        CategoryButtonObjects[5] = GameObject.Find("6s");
        CategoryButtonObjects[6] = GameObject.Find("3OfAKind");
        CategoryButtonObjects[7] = GameObject.Find("4OfAKind");
        CategoryButtonObjects[8] = GameObject.Find("FullHouse");
        CategoryButtonObjects[9] = GameObject.Find("SmallStraight");
        CategoryButtonObjects[10] = GameObject.Find("LargeStraight");
        CategoryButtonObjects[11] = GameObject.Find("Chance");
        CategoryButtonObjects[12] = GameObject.Find("Yahtzee");
    }

    public void ReportDice(int Value, string Dice)
    {
        switch (Dice)
        {
            case "Dice1":
                DiceValues[0] = Value;
                break;
            case "Dice2":
                DiceValues[1] = Value;
                break;
            case "Dice3":
                DiceValues[2] = Value;
                break;
            case "Dice4":
                DiceValues[3] = Value;
                break;
            case "Dice5":
                DiceValues[4] = Value;
                break;
        }
    }

    public void GetReport()
    {
        for (int i = 0; i < CategoryCount; i++)
        {
            if (CategoryButtons[i].CategorySelected()) {
                CategoryButtons[i].ReportScore();
            }
        }
    }

    public void ReportScore(int CategoryScore, bool Upper, int CategoryID)
    {
        Score[PlayerTurn] += CategoryScore;
        if (Upper)
        {
            UpperScore[PlayerTurn] += CategoryScore;
            if (UpperScore[PlayerTurn] >= 63 && !SilverStars[PlayerTurn])
            {
                Score[PlayerTurn] += SilverBonus;
                SilverStars[PlayerTurn] = true;
                SilverStar.SetActive(true);
            }
        }
        if (HighestDiceCount == NumberOfDice && CategoryStates[CategoryCount - 1, PlayerTurn] == 1)
        {
            Score[PlayerTurn] += GoldBonus;
            GoldStars[PlayerTurn]++;
            Scoreboard.text = "Score: " + Score[PlayerTurn].ToString() + " (" + UpperScore[PlayerTurn].ToString() + ")";
            BonusCounter.text = "X " + GoldStars[PlayerTurn].ToString();
            GoldStar.SetActive(true);
        }
        if (CategoryScore > 0)
        {
            CategoryStates[CategoryID, PlayerTurn] = 1;
        }
        else
        {
            CategoryStates[CategoryID, PlayerTurn] = -1;
        }
        Scoreboard.text = "Score: " + Score[PlayerTurn].ToString() + " (" + UpperScore[PlayerTurn].ToString() + ")";
        StartCoroutine(WaitForNextTurn());
    }

    public void Calculate()
    {
        for (int i = 0; i < PossibleDiceValues; i++)
        {
            DiceCount[i] = 0;
        }
        for (int i = 0; i < NumberOfDice; i++)
        {
            DiceCount[DiceValues[i] - 1]++;
        }
        HighestDiceCount = MaxValue();
        Sequence = MaxStraight();
        FullHouse = FullHouseCheck();
        if (HighestDiceCount == NumberOfDice && CategoryStates[CategoryCount - 1, PlayerTurn] != 0)
        {
            if (CategoryStates[DiceValues[0] - 1, PlayerTurn] != 0)
            {
                Joker = true;
            }
            else
            {
                Joker = false;
            }
        }
        else
        {
            Joker = false;
        }
    }

    public int RollValue(string Option)
    {
        SelectedCategory = Option;
        switch (Option)
        {
            case "1s":
                Scoreboard.text = "Score + " + DiceCount[0].ToString();
                return DiceCount[0];
            case "2s":
                Scoreboard.text = "Score + " + (DiceCount[1] * 2).ToString();
                return DiceCount[1] * 2;
            case "3s":
                Scoreboard.text = "Score + " + (DiceCount[2] * 3).ToString();
                return DiceCount[2] * 3;
            case "4s":
                Scoreboard.text = "Score + " + (DiceCount[3] * 4).ToString();
                return DiceCount[3] * 4;
            case "5s":
                Scoreboard.text = "Score + " + (DiceCount[4] * 5).ToString();
                return DiceCount[4] * 5;
            case "6s":
                Scoreboard.text = "Score + " + (DiceCount[5] * 6).ToString();
                return DiceCount[5] * 6;
            case "3OfAKind":
                if (HighestDiceCount >= 3)
                {
                    Scoreboard.text = "Score + " + ChanceValue().ToString();
                    return ChanceValue();
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            case "4OfAKind":
                if (HighestDiceCount >= 4)
                {
                    Scoreboard.text = "Score + " + ChanceValue().ToString();
                    return ChanceValue();
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            case "FullHouse":
                if (FullHouse || Joker)
                {
                    Scoreboard.text = "Score + 25";
                    return FullHouseScore;
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            case "SmallStraight":
                if (Sequence >= MinStraight || Joker)
                {
                    Scoreboard.text = "Score + 30";
                    return SmallStraightScore;
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            case "LargeStraight":
                if (Sequence > MinStraight || Joker)
                {
                    Scoreboard.text = "Score + 40";
                    return LargeStraightScore;
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            case "Chance":
                Scoreboard.text = "Score + " + ChanceValue().ToString();
                return ChanceValue();
            case "Yahtzee":
                if (HighestDiceCount == NumberOfDice)
                {
                    Scoreboard.text = "Score + 50";
                    return YahtzeeScore;
                }
                else
                {
                    Scoreboard.text = "Score + 0";
                    return 0;
                }
            default:
                Scoreboard.text = "Score + 0";
                return 0;
        }
    }

    public void WildUpdate()
    {
        Calculate();
        for (int i = 0; i < CategoryCount; i++)
        {
            if (CategoryButtons[i].CategorySelected())
            {
                UpdateRollValue();
            }
        }
    }

    public void UpdateRollValue()
    {
        int CategoryNum;
        switch (SelectedCategory)
        {
            case "1s":
                CategoryNum = 0;
                break;
            case "2s":
                CategoryNum = 1;
                break;
            case "3s":
                CategoryNum = 2;
                break;
            case "4s":
                CategoryNum = 3;
                break;
            case "5s":
                CategoryNum = 4;
                break;
            case "6s":
                CategoryNum = 5;
                break;
            case "3OfAKind":
                CategoryNum = 6;
                break;
            case "4OfAKind":
                CategoryNum = 7;
                break;
            case "FullHouse":
                CategoryNum = 8;
                break;
            case "SmallStraight":
                CategoryNum = 9;
                break;
            case "LargeStraight":
                CategoryNum = 10;
                break;
            case "Chance":
                CategoryNum = 11;
                break;
            case "Yahtzee":
                CategoryNum = 12;
                break;
            default:
                CategoryNum = -1;
                break;
        }
        CategoryButtons[CategoryNum].UpdateValue(RollValue(SelectedCategory));
    }

    public void OpenSelections()
    {
        for (int i = 0; i < CategoryCount; i++)
        {
            if (CategoryStates[i, PlayerTurn] == 0)
            {
                CategoryButtons[i].SetAvailability(true);
            }
        }
    }

    public void CloseSelections()
    {
        ClearSelection();
        for (int i = 0; i < CategoryCount; i++)
        {
            CategoryButtons[i].SetAvailability(false);
        }
    }

    public void ClearSelection()
    {
        for (int i = 0; i < CategoryCount; i++)
        {
            if (CategoryStates[i,PlayerTurn] == 0)
            {
                CategoryButtons[i].DeselectCategory();
            }
        }
        Scoreboard.text = "Score: " + Score[PlayerTurn].ToString() + " (" + UpperScore[PlayerTurn].ToString() + ")";
    }

    IEnumerator WaitForNextTurn()
    {
        StatusIndicator.text = "Category selected";
        Receptor.ResetHoldsAndWilds();
        yield return new WaitForSeconds(3);
        NextTurn();
    }

    int ChanceValue()
    {
        int TotalValue = 0;
        for (int i = 0; i < NumberOfDice; i++)
        {
            TotalValue += DiceValues[i];
        }
        return TotalValue;
    }

    int MaxValue()
    {
        int Mode = DiceCount[0];
        for (int i = 1; i < PossibleDiceValues; i++)
        {
            if (DiceCount[i] > Mode)
            {
                Mode = DiceCount[i];
            }
        }
        return Mode;
    }

    int MaxStraight()
    {
        int CurrentSequence = 1;
        int MaxSequence = 1;
        for (int i = 0; i < PossibleDiceValues - 1; i++)
        {
            if (DiceCount[i] >= 1 && DiceCount[i+1] >= 1)
            {
                CurrentSequence++;
                if (CurrentSequence > MaxSequence)
                {
                    MaxSequence = CurrentSequence;
                }
            }
            else
            {
                CurrentSequence = 1;
            }
        }
        return MaxSequence;
    }

    bool FullHouseCheck()
    {
        bool Three = false;
        bool Two = false;
        for (int i = 0; i < PossibleDiceValues; i++)
        {
            if (DiceCount[i] == 3)
            {
                Three = true;
            }
            else if (DiceCount[i] == 2)
            {
                Two = true;
            }
            if (Three && Two)
            {
                return true;
            }
        }
        return false;
    }

    void NextTurn()
    {
        CloseSelections();
        PlayerTurn = (PlayerTurn + 1) % PlayerCount;
        if (PlayerTurn == FirstPlayer)
        {
            TurnNumber++;
        }
        if (TurnNumber > TurnLimit)
        {
            ISC.EndGame(Score);
        }
        else
        {
            Scoreboard.text = "Score: " + Score[PlayerTurn].ToString() + " (" + UpperScore[PlayerTurn].ToString() + ")";
            if (SilverStars[PlayerTurn])
            {
                SilverStar.SetActive(true);
            }
            else
            {
                SilverStar.SetActive(false);
            }
            if (GoldStars[PlayerTurn] == 0)
            {
                GoldStar.SetActive(false);
            }
            else
            {
                GoldStar.SetActive(true);
            }
            BonusCounter.text = "X " + GoldStars[PlayerTurn].ToString();
            PlayerUp.text = Names[PlayerTurn];
            //PlayerUp.text = "Player " + (PlayerTurn + 1).ToString();
            for (int i = 0; i < CategoryCount; i++)
            {
                CategoryButtons[i].SetStatus(CategoryStates[i, PlayerTurn]);
            }
            Receptor.ResetRolls();
        }
    }

}
