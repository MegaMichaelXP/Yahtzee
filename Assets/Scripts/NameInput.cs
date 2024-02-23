using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    private InputField IF;

    private IntersceneController ISC;
    // Start is called before the first frame update
    void Start()
    {
        ISC = GameObject.Find("IntersceneController").GetComponent<IntersceneController>();
        IF = gameObject.GetComponent<InputField>();
        IF.onEndEdit.AddListener(delegate { SendName(); });
    }

    void SendName()
    {
        ISC.SetName(IF.text, gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
