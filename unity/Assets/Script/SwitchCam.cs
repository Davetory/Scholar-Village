using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCam : MonoBehaviour
{
    Camera MainCam;
    public Camera FullViewCam;
    public GameObject focusbutton;
    public GameObject[] characters;

    void Start()
    {
        MainCam = Camera.main;
        MainCam.enabled = true;
        FullViewCam.enabled = false;
        focusbutton.SetActive(false);
    }
    public void ShowFullView()
    {
        MainCam.enabled = false;
        FullViewCam.enabled = true;
        focusbutton.SetActive(true);
        characters[0].GetComponent<movement>().enabled = false;
        characters[1].GetComponent<movement>().enabled = false;

    }
    
    public void ShowFocusedView()
    {
        MainCam.enabled = true;
        FullViewCam.enabled = false;
        focusbutton.SetActive(false);
        characters[0].GetComponent<movement>().enabled = true;
        characters[1].GetComponent<movement>().enabled = true;
    }
}
