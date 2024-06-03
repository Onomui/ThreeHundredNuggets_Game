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
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{
    private StringBuilder AnimateText = new();
    private string[] ForFirst = new[]
    {
        "    Для доктора Бургермена настал счастливый конец,\nон все также кормил всех людей своим фастфудом.",
        "    Поначалу все было хорошо, но со временем люди стали\nстрадать от всяческих болезней и толстеть до таких размеров,\nчто их тела не выдерживали и лопались словно воздушные шарики.",
        "    Человечеству оставалось надеяться и ждать только того,\nчто на замену зловещему диктатору придет герой,\nпродвигающий здоровый образ жизни.",
        "    ПОЗДРАВЛЯЕМ,\nВЫ УСПЕШНО ПОРАБОТИЛИ ВСЕ ЧЕЛОВЕЧЕСТВО И ПРОШЛИ ИГРУ!"
    };
    
    private string[] dialogueLines;
    private int dialogueLineNum = 0;
    private Label dialogueText;
    private VisualElement enemyPic;
    public int LevelNum = 1;
    private Coroutine displayCoroutine;
    private bool isTyping = true;
    public bool isSkipRequested;
    [SerializeField] private float typingSpeed = 1.04f;
    public Sprite egorPic;
    private gameOverScript gameOver;

    [SerializeField] private GameObject tutor;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var allLines = new[] { ForFirst };

        dialogueLines = allLines[0];



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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[dialogueLineNum]));
        }
    }
}
