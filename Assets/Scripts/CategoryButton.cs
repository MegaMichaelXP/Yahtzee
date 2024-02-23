using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    private const float FlashInterval = 0.5f;

    private Controller GameController;
    private KeyReceptor Receptor;

    private Button CButton;
    private Image ButtonColor;

    private int CategoryID;
    private int ReportedValue;
    private float TimeStamp;
    private bool Upper;
    private bool Selected;
    private bool Available;
    private bool Flash;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("Controller").GetComponent<Controller>();
        Receptor = GameObject.Find("KeyReceptor").GetComponent<KeyReceptor>();
        CButton = gameObject.GetComponent<Button>();
        CButton.onClick.AddListener(SelectCategory);
        ButtonColor = CButton.GetComponent<Image>();
        ButtonColor.color = Color.white;
        ReportedValue = 0;
        Available = false;
        Selected = false;
        Flash = false;
        switch (gameObject.name)
        {
            case "1s":
                CategoryID = 0;
                break;
            case "2s":
                CategoryID = 1;
                break;
            case "3s":
                CategoryID = 2;
                break;
            case "4s":
                CategoryID = 3;
                break;
            case "5s":
                CategoryID = 4;
                break;
            case "6s":
                CategoryID = 5;
                break;
            case "3OfAKind":
                CategoryID = 6;
                break;
            case "4OfAKind":
                CategoryID = 7;
                break;
            case "FullHouse":
                CategoryID = 8;
                break;
            case "SmallStraight":
                CategoryID = 9;
                break;
            case "LargeStraight":
                CategoryID = 10;
                break;
            case "Chance":
                CategoryID = 11;
                break;
            case "Yahtzee":
                CategoryID = 12;
                break;
            default:
                CategoryID = -1;
                break;
        }
        if (CategoryID <= 5)
        {
            Upper = true;
        }
        else
        {
            Upper = false;
        }
        TimeStamp = Time.time;
    }

    public void SetAvailability(bool Availability)
    {
        Available = Availability;
    }

    public void UpdateValue(int NewValue)
    {
        ReportedValue = NewValue;
    }

    void SelectCategory()
    {
        //TimeStamp = Time.time;
        if (Available && !Selected)
        {
            GameController.ClearSelection();
            Receptor.ReportSelection();
            ReportedValue = GameController.RollValue(gameObject.name);
            Selected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            if (Time.time - TimeStamp >= FlashInterval)
            {
                if (!Flash)
                {
                    if (ReportedValue > 0)
                    {
                        ButtonColor.color = Color.green;
                    }
                    else
                    {
                        ButtonColor.color = Color.red;
                    }
                    Flash = true;
                }
                else
                {
                    ButtonColor.color = Color.white;
                    Flash = false;
                }
                TimeStamp = Time.time;
            }
        }
    }

    public void DeselectCategory()
    {
        Selected = false;
        Flash = false;
        SetStatus(0);
    }

    public void SetStatus(int Status)
    {
        switch (Status)
        {
            case 1:
                ButtonColor.color = Color.green;
                break;
            case -1:
                ButtonColor.color = Color.red;
                break;
            default:
                ButtonColor.color = Color.white;
                break;
        }
    }

    public void ReportScore()
    {
        if (Selected)
        {
            if (ReportedValue > 0)
            {
                SetStatus(1);
            }
            else
            {
                SetStatus(-1);
            }
            Flash = false;
            GameController.ReportScore(ReportedValue, Upper, CategoryID);
        }
        Selected = false;
    }

    public bool CategorySelected()
    {
        return Selected;
    }
}
