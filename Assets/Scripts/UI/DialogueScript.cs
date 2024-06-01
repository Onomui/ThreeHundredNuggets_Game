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
        "   В далёком будущем на нашей планете царит мир и процветание. Люди живут долго и счастливо, \nони научились грамотно рассчитывать калории, каждый следит за своим рационом \nпитания и регулярно занимается физической активностью.",
        "   Доктор Бургермен мечтает о мире, в котором люди не должны ограничивать себя \nв потреблении вкусной, жирной и калорийной пищи. Он создает мощный пищевой \nробот Фастфудиум, способный производить невероятно вкусные и калорийные блюда. \nОн начинает свою миссию по распространению пищевого изобилия среди населения Земли.",
        "  \"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками своих желудков, \nа я буду править ими с помощью моего замечательного Фастфудиума! \nУахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\"",
        "   Помогите Доктору Бургермену осуществить его злодейский план, не дайте людям дойти \nдо тренажёрного зала, расставляя башни Фастфудиума по всей карте."
    };
    private string[] ForSecond = new[]
    {
        "   Бургермен сталкивается с первыми препятствиями со стороны людей, которые \nхотят сохранить мир и процветание, достигнутые благодаря здоровому образу \nжизни. Он принимает решительные меры против оппонентов, \nчтобы продвинуть свою идеологию ожирения.",
        "  \"Те, кто осмеливаются противиться моему величию, будут уничтожены в вихре \nкалорийного вкуса! Никто не остановит меня на пути к победе над здоровым \nпитанием!\"",
        "  \"У тебя не получится захватить власть в этом городе! Спорт и сила - ты могила!\""
    };
    private string[] ForThird = new[]
{
        "   Бургермен утверждает свою власть и начинает систематически уничтожать всех, \nкто остается верен здоровому образу жизни. Он предлагает людям выбор - принять идеологию \nожирения или быть уничтоженными.",
        "  \"ХАХАХАА, в мире почти не осталось фитнес-клубов и рестаранов здорового питания! \nУже почти все люди имеют 2ую степень ожирения!\"",
        "   Босс ЕГОР: \"Бургермен, твоя тирания ужасна, я всю жизнь жил по устоям, которые \nты пропогандируешь, но сейчас я понял, что это всё неправильно! Я не дам тебе \nзахватить мир и обязательно начну заниматься спортом и следить за своим питанием!\""
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
