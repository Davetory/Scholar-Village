using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quiz.UI;

public class QuizDemo : MonoBehaviour
{
    public string message;

    void Start()
    {
        QuizUI.Instance.Hide();
    }

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            QuizUI.Instance.SetTitle(message).Show();
        }
    }

    void OnDialogClose()
    {

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            QuizUI.Instance.Hide();
        }
    }
}
