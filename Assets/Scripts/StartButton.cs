using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button PButton;
    // Start is called before the first frame update
    void Start()
    {
        PButton = gameObject.GetComponent<Button>();
        PButton.onClick.AddListener(NewGame);
    }

    void NewGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
