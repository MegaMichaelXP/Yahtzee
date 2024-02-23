using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    private const int MaxValue = 6;

    private float TimeStamp;
    private bool Rolling;
    private bool Holdable;
    private bool Holding;
    private bool Wild;
    private int Value;
    private List<Sprite> Faces = new List<Sprite>();
    private Image FaceImage;
    private Button DiceButton;
    private GameObject HoldIndicator;
    private GameObject WildIndicator;
    private Controller GameController;

    private KeyReceptor Receptor;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("Controller").GetComponent<Controller>();
        Receptor = GameObject.Find("KeyReceptor").GetComponent<KeyReceptor>();
        DiceButton = gameObject.GetComponent<Button>();
        DiceButton.onClick.AddListener(DiceClick);
        FaceImage = DiceButton.GetComponent<Image>();
        HoldIndicator = gameObject.transform.GetChild(0).gameObject;
        HoldIndicator.SetActive(false);
        WildIndicator = gameObject.transform.GetChild(1).gameObject;
        WildIndicator.SetActive(false);
        Holdable = false;
        Holding = false;
        Rolling = false;
        Wild = false;
        TimeStamp = Time.time;
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice1"));
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice2"));
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice3"));
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice4"));
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice5"));
        Faces.Add(Resources.Load<Sprite>("Sprites/Dice6"));
        SetValue(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Value == 0)
        {
            if (Time.time - TimeStamp >= 0.1f)
            {
                TimeStamp = Time.time;
                FaceImage.sprite = Faces[Random.Range(0, Faces.Count)];
            }
        }
    }

    void DiceClick()
    {
        if (Wild && !Rolling)
        {
            if (Value == MaxValue)
            {
                SetValue(1);
            }
            else
            {
                SetValue(Value + 1);
            }
            Report();
            GameController.WildUpdate();
        }
        else
        {
            if (Holdable && !Rolling)
            {
                if (Holding)
                {
                    Holding = false;
                    HoldIndicator.SetActive(false);
                    Receptor.ReportUnhold();
                }
                else
                {
                    Holding = true;
                    HoldIndicator.SetActive(true);
                    Receptor.ReportHold();
                }
            }
        }
    }

    public void SetValue(int NewValue)
    {
        Value = NewValue;
        if (Value != 0)
        {
            FaceImage.sprite = Faces[NewValue - 1];
        }
    }

    public void SetHoldable(bool IsHoldable)
    {
        Holdable = IsHoldable;
    }

    public void SetRollingStatus(bool RollStatus)
    {
        Rolling = RollStatus;
    }

    public void MakeWild()
    {
        Wild = true;
        WildIndicator.SetActive(true);
    }

    public bool Held()
    {
        return Holding;
    }

    public void Reset()
    {
        Holding = false;
        HoldIndicator.SetActive(false);
        Wild = false;
        WildIndicator.SetActive(false);
    }

    public void Report()
    {
        GameController.ReportDice(Value, gameObject.name);
    }

}
