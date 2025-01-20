using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ExcelReader : MonoBehaviour

//aquest script actualitzara coses de la ui, per aixo necessitem el tmpro i unityengine
{
    public string csv = "qa"; //nom del fitxer csv amb les qa
    public List<string> Answers, Questions; //llista de strings (preguntes i respostes)
    public Button questionButton; //acces als buttons, cada pregunta es un boto. boto de 
    //referencia dins un panel (canva->panel(escalarlo, vertical layout group(upperleft))->buton)
    public TextMeshProUGUI answerText;
    void Start()
    {
        TextAsset text = Resources.Load<TextAsset>(csv); //de la carpeta i el resources agafes la info
        //i la posa dins de la variable
        if(csv!= null)
        {
            ReadCSV(text.text);
            answerText.text = "";
        }
    }
    private void ReadCSV(string csv)
    {
        string[] rows = csv.Split("\n");//array de strings 
        //iteració. totes les preguntes
        for (int i=0; i<rows.Length; i++)//tenim les preguntes i respostes separades per linies
        {
            string[] cells = rows[i].Split(","); //per cada linia se separa amb la , (cells:2 pregunta i resposta)
            Questions.Add(cells[0]); //posicio preguntes
            Button newQButton = Instantiate(questionButton, questionButton.transform.parent);
            newQButton.GetComponentInChildren<TextMeshProUGUI>().text = cells[0]; //agafa la component (fill)textmeshpro del boto i mostra el text
            var currentIndex = i; 
            newQButton.onClick.AddListener(() => AnswerTheQuestion(currentIndex)); 
            /*newQButton.onClick.AddListener(delegate { 
                AnswerTheQuestion(currentIndex);
            } );*/
            Answers.Add(cells[1]);//posicio respostes
        }
        questionButton.gameObject.SetActive(false);
    }
    public void AnswerTheQuestion(int i)
    {
        answerText.text = Answers[i]; //amb la i sempre donara la resposta correcta
    }
}
