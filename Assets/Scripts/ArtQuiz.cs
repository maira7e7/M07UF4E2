using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtQuiz : MonoBehaviour
{
    public ArtScriptableObjects currentQuestion; 
    public Image Sprite1, Sprite2; 
    public Button Option1, Option2; 
    public TextMeshProUGUI resultText;

    void Start()
    {
        LoadQuestion();
    }

    void LoadQuestion()
    {
       
        Sprite1.sprite = currentQuestion.Sprite1;
        Sprite2.sprite = currentQuestion.Sprite2;

        
        Option1.onClick.AddListener(() => CheckAnswer(true));
        Option2.onClick.AddListener(() => CheckAnswer(false));
    }

    void CheckAnswer(bool selectedFirstImage)
    {
       
        if (selectedFirstImage == currentQuestion.isFirstImageFake)
        {
            resultText.text = "Wrong answer.";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "Correct!";
            resultText.color = Color.green;
        }

        
        Option1.interactable = false;
        Option2.interactable = false;
    }
}