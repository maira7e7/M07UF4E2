using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtQuiz : MonoBehaviour
{
    public ArtScriptableObjects[] questions;
    private int currentIndex = 0;

    public Image Sprite1, Sprite2;
    public Button Option1, Option2, NextButton, ExitButton;
    public TextMeshProUGUI resultText;
    public GameObject quizCanvas; // Referencia al Canvas

    void Start()
    {
        NextButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentIndex >= questions.Length) // Si el quiz termina
        {
            resultText.text = "¡Fin del quiz!";
            NextButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(true); // Mostrar botón de salir
            Option1.gameObject.SetActive(false);
            Option2.gameObject.SetActive(false);
            return;
        }

        ArtScriptableObjects currentQuestion = questions[currentIndex];

        Sprite1.sprite = currentQuestion.Sprite1;
        Sprite2.sprite = currentQuestion.Sprite2;

        Option1.onClick.RemoveAllListeners();
        Option2.onClick.RemoveAllListeners();
        NextButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();

        Option1.onClick.AddListener(() => CheckAnswer(currentQuestion, true));
        Option2.onClick.AddListener(() => CheckAnswer(currentQuestion, false));
        NextButton.onClick.AddListener(NextQuestion);
        ExitButton.onClick.AddListener(CloseQuiz);

        Option1.interactable = true;
        Option2.interactable = true;
        NextButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
    }

    void CheckAnswer(ArtScriptableObjects question, bool selectedFirstImage)
    {
        if (selectedFirstImage == question.isFirstImageFake)
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
        NextButton.gameObject.SetActive(true);
    }

    void NextQuestion()
    {
        currentIndex++;
        LoadQuestion();
    }

    void CloseQuiz()
    {
        quizCanvas.SetActive(false); // Oculta el Canvas del quiz
    }
}
