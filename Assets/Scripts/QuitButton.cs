using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button QButton;
    // Start is called before the first frame update
    void Start()
    {
        QButton = gameObject.GetComponent<Button>();
        QButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
