using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueScript : MonoBehaviour
{
    private string[] ForFirst = new[]
    {
        "Доктор Бургермен создает мощного пищевого робота Фастфудиум, способного \nпроизводить невероятно вкусные и калорийные блюда. Он начинает свою миссию\n по распространению пищевого изобилия среди населения Земли.",
        "\"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками \nсвоих желудков, а я буду править ними с помощью моего замечательного \nФастфудиума! Уахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\""
    };
    private string[] ForSecond = new[]
    {
        "Бургермен сталкивается с первыми препятствиями со стороны людей, которые \nхотят сохранить мир и процветание, достигнутые благодаря здоровому образу \nжизни. Он принимает решительные меры против оппонентов, \nчтобы продвинуть свою идеологию ожирения.",
        "\"Те, кто осмеливаются противиться моему величию, будут уничтожены в вихре \nкалорийного вкуса! Никто не остановит меня на пути к победе над здоровым \nпитания!\"",
        "\"У тебя не получится захватить власть в этом городе! Спорт сила - ты могила\""
    };
    private string[] dialogueLines;
    private int dialogueLineNum = 0;
    private Label dialogueText;
    public int LevelNum = 1;

    private void OnEnable()
    {
        var allLines = new[] { ForFirst, ForSecond };
        dialogueLines = allLines[LevelNum - 1];


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
