using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using System.Timers;
using UnityEditor;

public class DialogueScript : MonoBehaviour
{
    private StringBuilder AnimateText = new();
    private string[] ForFirst = new[]
    {
        "Доктор Бургермен создает мощного пищевого робота Фастфудиум, способного производить невероятно вкусные и калорийные блюда.\n Он начинает свою миссию по распространению пищевого изобилия среди населения Земли.",
        "\"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками своих желудков,\n а я буду править ними с помощью моего замечательного Фастфудиума!\n Уахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\""
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
    private Coroutine displayCoroutine;
    [SerializeField] private float typingSpeed = 1.04f;

    [SerializeField] private GameObject tutor;
    private void OnEnable()
    {
        var allLines = new[] { ForFirst, ForSecond };
        dialogueLines = allLines[LevelNum - 1];


        var root = GetComponent<UIDocument>().rootVisualElement;
        

        var dialogueButton = root.Q<Button>("dialogueButton");
        dialogueText = root.Q<Label>("dialogueText");
        displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[dialogueLineNum]));
        dialogueButton.clicked += DialogueButton_clicked;
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        foreach (var letter in line)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(displayCoroutine);
                dialogueText.text = dialogueLines[dialogueLineNum];
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    

    private void DialogueButton_clicked()
    {
        StopCoroutine(displayCoroutine);
        dialogueLineNum++;
        if (dialogueLineNum == dialogueLines.Length)
        {
            if (tutor != null)
                Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("tutorial");
            else
                Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("hud");
            return;
        }

        displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[dialogueLineNum]));
    }
}
