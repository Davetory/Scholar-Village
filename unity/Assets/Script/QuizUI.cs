using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.UI
{
    public class Dialog
    {
        public string Title;
    }
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Text titleUIText;
        [SerializeField] Button connectUIButton;
        [SerializeField] Button closeUIButton;
        Dialog dialog = new Dialog();
        public static QuizUI Instance;

        void Awake()
        {
            Instance = this;

            closeUIButton.onClick.RemoveAllListeners();
            closeUIButton.onClick.AddListener(Hide);
        }

        public QuizUI SetTitle (string title)
        {
            dialog.Title = title;
            return Instance;
        }

        public void Show()
        {
            titleUIText.text = dialog.Title;

            dialog = new Dialog();
            canvas.SetActive(true);
        }

        //Hide dialog
        public void Hide()
        {
            canvas.SetActive(false);

            //Reset Dialog
            dialog = new Dialog();
        }
    }
}