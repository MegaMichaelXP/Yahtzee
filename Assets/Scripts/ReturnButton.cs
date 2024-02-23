using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    private Button RButton;

    private IntersceneController ISC;
    // Start is called before the first frame update
    void Start()
    {
        RButton = gameObject.GetComponent<Button>();
        RButton.onClick.AddListener(ReturnToMenu);
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        ISC.ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
