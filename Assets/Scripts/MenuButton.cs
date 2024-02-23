using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button MButton;

    private IntersceneController ISC;

    private MenuController Menu;
    // Start is called before the first frame update
    void Start()
    {
        MButton = gameObject.GetComponent<Button>();
        MButton.onClick.AddListener(StartGame);
        Menu = GameObject.Find("MenuController").GetComponent<MenuController>();
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
    }

    void StartGame()
    {
        ISC.SetPlayerCount(gameObject.name);
        Menu.PreGame(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
