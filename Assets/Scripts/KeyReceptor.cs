using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyReceptor : MonoBehaviour
{
    private const int UpperValue = 7;
    private const int DiceCount = 5;

    private const int WildChance = 10;

    private GameObject InGameMenu;

    private Controller GameController;
    private IntersceneController ISC;

    private GameObject[] DiceObjects;
    private DiceController[] Dice;
    private UnityEngine.UI.Text StatusIndicator;
    private bool ActiveRoll;
    private bool[] Rolling;
    private bool[] Holding;
    private bool[] Wild;
    private float TimeStamp;
    private int Rolls;
    private int Holds;
    private int WildCount;
    private int WildCheck;
    
    private bool InputAvailable;
    private bool SelectedCategory;
    private bool WildMode;
    private bool Spacebar;
    private bool Enter;
    private bool IGM;
    // Start is called before the first frame update
    void Start()
    {
        Spacebar = false;
        Enter = false;
        ActiveRoll = false;
        InputAvailable = true;
        IGM = false;
        InGameMenu = GameObject.Find("InGameMenu");
        InGameMenu.SetActive(false);
        GameController = GameObject.Find("Controller").GetComponent<Controller>();
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        WildMode = ISC.WildStatus();
        Rolls = 0;
        Holds = 0;
        WildCheck = 0;
        WildCount = 0;
        Rolling = new bool[] { false, false, false, false, false };
        Holding = new bool[] { false, false, false, false, false };
        Wild = new bool[] { false, false, false, false, false };
        DiceObjects = GameObject.FindGameObjectsWithTag("Dice");
        /*
        DiceObjects = new GameObject[5];
        DiceObjects[0] = GameObject.Find("Dice1");
        DiceObjects[1] = GameObject.Find("Dice2");
        DiceObjects[2] = GameObject.Find("Dice3");
        DiceObjects[3] = GameObject.Find("Dice4");
        DiceObjects[4] = GameObject.Find("Dice5");
        */
        Dice = new DiceController[5];
        for (int i = 0; i < DiceCount; i++)
        {
            Dice[i] = DiceObjects[i].GetComponent<DiceController>();
        }
        StatusIndicator = GameObject.Find("Status").GetComponent<UnityEngine.UI.Text>();
    }

    void RollDice()
    {
        if (Rolls < 3 && !ActiveRoll && Holds + WildCount < DiceCount)
        {
            GameController.CloseSelections();
            ActiveRoll = true;
            for (int i = 0; i < DiceCount; i++)
            {
                Rolling[i] = true;
            }
            StatusIndicator.text = "Rolling...";
            for (int i = 0; i < DiceCount; i++)
            {
                if (!Holding[i] && !Wild[i])
                {
                    Dice[i].SetValue(0);
                }
            }
            for (int i = 0; i < DiceCount; i++)
            {
                Dice[i].SetRollingStatus(true);
            }
            TimeStamp = Time.time;
            Rolls++;
            if (Rolls == 1)
            {
                for (int i = 0; i < DiceCount; i++)
                {
                    Dice[i].SetHoldable(true);
                }
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (InputAvailable)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !Spacebar && !Rolling[4])
            {
                if (SelectedCategory)
                {
                    GameController.ClearSelection();
                    SelectedCategory = false;
                }
                else
                {
                    RollDice();
                    Spacebar = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) && !Enter && SelectedCategory)
            {
                InputAvailable = false;
                GameController.GetReport();
                SelectedCategory = false;
                Enter = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (IGM)
            {
                InGameMenu.SetActive(false);
                IGM = false;
            }
            else
            {
                InGameMenu.SetActive(true);
                IGM = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Spacebar = false;
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Enter = false;
        }
        if (ActiveRoll)
        {
            if (Rolling[0] && Time.time - TimeStamp >= 3.0f)
            {
                if (!Holding[0] && !Wild[0])
                {
                    Dice[0].SetValue(Random.Range(1, UpperValue));
                }
                Rolling[0] = false;
            }
            if (Rolling[1] && Time.time - TimeStamp >= 3.2f)
            {
                if (!Holding[1] && !Wild[1])
                {
                    Dice[1].SetValue(Random.Range(1, UpperValue));
                }
                Rolling[1] = false;
            }
            if (Rolling[2] && Time.time - TimeStamp >= 3.4f)
            {
                if (!Holding[2] && !Wild[2])
                {
                    Dice[2].SetValue(Random.Range(1, UpperValue));
                }
                Rolling[2] = false;
            }
            if (Rolling[3] && Time.time - TimeStamp >= 3.6f)
            {
                if (!Holding[3] && !Wild[3])
                {
                    Dice[3].SetValue(Random.Range(1, UpperValue));
                }
                Rolling[3] = false;
            }
            if (Rolling[4] && Time.time - TimeStamp >= 3.8f)
            {
                if (!Holding[4] && !Wild[4])
                {
                    Dice[4].SetValue(Random.Range(1, UpperValue));
                }
                Rolling[4] = false;
            }
            if (Time.time - TimeStamp >= 4.0f)
            {
                ActiveRoll = false;
                for (int i = 0; i < DiceCount; i++)
                {
                    Dice[i].SetRollingStatus(false);
                    Dice[i].Report();
                }
                if (WildMode)
                {
                    for (int i = 0; i < DiceCount; i++)
                    {
                        WildCheck = Random.Range(0, WildChance);
                        if (WildCheck == 0 && !Holding[i] && !Wild[i])
                        {
                            Dice[i].MakeWild();
                            Wild[i] = true;
                            WildCount++;
                        }
                    }
                }
                GameController.Calculate();
                GameController.OpenSelections();
                if (Rolls == 3 || WildCount == DiceCount)
                {
                    StatusIndicator.text = "Select category";
                    for (int i = 0; i < DiceCount; i++)
                    {
                        Dice[i].SetHoldable(false);
                    }
                }
                else
                {
                    if (Holds + WildCount == 5)
                    {
                        StatusIndicator.text = "Unhold dice or select category";
                    }
                    else
                    {
                        StatusIndicator.text = "Roll dice or select category";
                    }
                }
            }
        }
    }

    public void ReportHold()
    {
        Holds++;
        for (int i = 0; i < DiceCount; i++)
        {
            Holding[i] = Dice[i].Held();
        }
        if (Holds + WildCount == DiceCount)
        {
            StatusIndicator.text = "Unhold dice or select category";
        }
    }

    public void ReportUnhold()
    {
        Holds--;
        for (int i = 0; i < DiceCount; i++)
        {
            Holding[i] = Dice[i].Held();
        }
        if (Holds + WildCount < DiceCount)
        {
            StatusIndicator.text = "Roll dice or select category";
        }
    }

    public void ReportSelection()
    {
        SelectedCategory = true;
    }

    public void ResetHoldsAndWilds()
    {
        Holds = 0;
        WildCount = 0;
        for (int i = 0; i < DiceCount; i++)
        {
            Holding[i] = false;
            Wild[i] = false;
            Dice[i].Reset();
        }
    }

    public void ResetRolls()
    {
        StatusIndicator.text = "Roll dice";
        InputAvailable = true;
        Rolls = 0;
    }
}
