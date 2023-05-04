using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    public Text question;
    public string content;
    public GameObject NewWindow;
    public GameObject OldWindow;
    public GameObject reminder;

    void Start()
    {
        NewWindow.SetActive(false);
        question.text = content;

    }

    public void HidePanel()
    {
        NewWindow.SetActive(false);
        OldWindow.SetActive(false);
        reminder.SetActive(false);
    }
    
    public void ShowPanel()
    {
        NewWindow.SetActive(true);
    }
}
