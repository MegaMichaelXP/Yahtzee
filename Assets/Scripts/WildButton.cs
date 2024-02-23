using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildButton : MonoBehaviour
{
    private Button WButton;

    private IntersceneController ISC;

    private UnityEngine.UI.Text ButtonText;

    private bool Wild;
    // Start is called before the first frame update
    void Start()
    {
        WButton = gameObject.GetComponent<Button>();
        WButton.onClick.AddListener(ToggleWilds);
        ButtonText = WButton.GetComponentInChildren<UnityEngine.UI.Text>();
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        Wild = false;
    }

    void ToggleWilds()
    {
        ISC.ToggleWild();
        if (Wild)
        {
            Wild = false;
            ButtonText.text = "Classic";
        }
        else
        {
            Wild = true;
            ButtonText.text = "Wild";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
