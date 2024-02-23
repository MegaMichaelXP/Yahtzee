using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button BButton;

    MenuController Menu;
    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("MenuController").GetComponent<MenuController>();
        BButton = gameObject.GetComponent<Button>();
        BButton.onClick.AddListener(ReturnToMenu);
    }

    void ReturnToMenu()
    {
        Menu.LoadMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
