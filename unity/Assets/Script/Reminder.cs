using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reminder : MonoBehaviour
{
    public GameObject closetag;
    public GameObject reminder;
    // Start is called before the first frame update
    void Start()
    {
        reminder.SetActive(false);
        closetag.SetActive(false);
    }

    public void Show()
    {
        reminder.SetActive(true);
        closetag.SetActive(true);
    }

    public void Hide()
    {
        reminder.SetActive(false);
        closetag.SetActive(false);
    }

}
