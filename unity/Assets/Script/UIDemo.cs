using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;

public class UIDemo : MonoBehaviour
{
    public string message;

    void Start()
    {
        DialogUI.Instance.Hide();
    }

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            DialogUI.Instance.SetTitle(message).Show();
        }
    }

    void OnDialogClose()
    {

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            DialogUI.Instance.Hide();
        }
    }
}
