using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueScript : MonoBehaviour
{
    private string[] dialogueLines = new[]
    {
        "Доктор Бургермен создает мощного пищевого робота Фастфудиум, способного \nпроизводить невероятно вкусные и калорийные блюда. Он начинает свою миссию\n по распространению пищевого изобилия среди населения Земли.",
        "\"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками \nсвоих желудков, а я буду править ними с помощью моего замечательного \nФастфудиума! Уахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\""
    };
    private int dialogueLineNum = 0;
    private Label dialogueText;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        

        var dialogueButton = root.Q<Button>("dialogueButton");
        dialogueText = root.Q<Label>("dialogueText");
        
        dialogueText.text = dialogueLines[dialogueLineNum];

        dialogueButton.clicked += DialogueButton_clicked;
    }

    private void DialogueButton_clicked()
    {
        dialogueLineNum++;
        if (dialogueLineNum == dialogueLines.Length)
        {
            Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("hud");
            return;
        }
        dialogueText.text = dialogueLines[dialogueLineNum];
    }
}
