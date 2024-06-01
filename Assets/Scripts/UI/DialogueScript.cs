using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using System.Timers;
using Unity.VisualScripting;
using UnityEditor;

public class DialogueScript : MonoBehaviour
{
    private StringBuilder AnimateText = new();
    private string[] ForFirst = new[]
    {
        "\\\\\\nпроизводить невероятно вкусные и  калорийные блюда. Он начинает\nсвою миссию по распространению пищевого изобилия среди населения Земли.",
        "\"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками\nсвоих желудков, а я буду править ними с помощью моего замечательного\nФастфудиума! Уахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\""
    };
    private string[] ForSecond = new[]
    {
        "Бургермен сталкивается с первыми препятствиями со стороны людей,\nкоторые хотят сохранить мир и процветание, достигнутые благодаря\nздоровому образу жизни. Он принимает решительные меры против оппонентов,\nчтобы продвинуть свою идеологию ожирения.",
        "\"Те, кто осмеливаются противиться моему величию, будут уничтожены в вихре \nкалорийного вкуса! Никто не остановит меня на пути к победе над здоровым \nпитания!\"",
        "\"У тебя не получится захватить власть в этом городе!\nСпорт сила - ты могила\""
    };
    private string[] ForThird = new[]
{
        "Бургермен утверждает свою власть и начинает систематически уничтожать\nвсех, кто остается верен здоровому образу жизни. Он предлагает\nлюдям выбор - принять идеологию ожирения или быть уничтоженными.",
        "\"ХАХАХАА, Почти не осталось Фитнесс залов и вся власть перешла ко мне!\nПочти все люди уже имеют 2 степень ожирения!\"",
        "приспешник босса: \"БургерМен, твоя тирания ужасна, ты поработил все\nинфраструктуры и превратил их во всяческие забегаловки, люди не\nдолжны так жить, освободи всех по хорошему\""
    };
    private string[] dialogueLines;
    private int dialogueLineNum = 0;
    private Label dialogueText;
    public int LevelNum = 1;
    private Coroutine displayCoroutine;
    private bool isTyping = true;
    public bool isSkipRequested;
    [SerializeField] private float typingSpeed = 1.04f;

    [SerializeField] private GameObject tutor;
    private void OnEnable()
    {
        var allLines = new[] { ForFirst, ForSecond, ForThird };
        dialogueLines = allLines[LevelNum - 1];


        var root = GetComponent<UIDocument>().rootVisualElement;
        

        var dialogueButton = root.Q<Button>("dialogueButton");
        dialogueText = root.Q<Label>("dialogueText");
        displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[dialogueLineNum]));
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        isSkipRequested = false;
        foreach (var letter in line)
        {
            if (isSkipRequested)
            {
                dialogueText.text = dialogueLines[dialogueLineNum];
                isSkipRequested = false;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTyping)
            {
                DialogueButton_clicked();
                isTyping = true; 
            }
            else
                SkipText();
        }
    }

    private void SkipText() => isSkipRequested = true;
    
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
