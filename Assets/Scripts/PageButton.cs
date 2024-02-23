using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageButton : MonoBehaviour
{
    private Button MButton;

    private MenuController Menu;
    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("MenuController").GetComponent<MenuController>();
        MButton = gameObject.GetComponent<Button>();
        MButton.onClick.AddListener(ChangePage);
    }

    void ChangePage()
    {
        switch (gameObject.name)
        {
            case "PreviousButton":
                Menu.PrevGuidePage();
                break;
            case "NextButton":
                Menu.NextGuidePage();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
