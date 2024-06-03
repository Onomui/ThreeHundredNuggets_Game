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
        "   В далёком будущем на нашей планете царит мир и процветание. Люди\nживут долго и счастливо, они научились грамотно рассчитывать\nкалории, каждый следит за своим рационом питания\nи регулярно занимается физической активностью.",
        "   Доктор Бургермен мечтает о мире, в котором люди не должны\nограничивать себя в потреблении вкусной, жирной и калорийной пищи.",
        "   Он создаетмощный пищевой робот Фастфудиум, способный производить\nневероятно вкусные и калорийные блюда. Он начинает свою миссию\nпо распространению пищевого изобилия среди населения\nЗемли.",
        "  \"Слава и вкус будут править этим миром! Пусть люди окажутся пленниками\nсвоих желудков, а я буду править ими с помощью моего замечательного\nФастфудиума! Уахахаахахх, СОСИТЕ КОКА КОЛУ ИЗ МОИХ ТРУБОЧЕК!\"",
        "   Помогите Доктору Бургермену осуществить его злодейский план,\nне дайте людям дойти до тренажёрного зала, расставляя\nбашни Фастфудиума по всей карте.",
        "   Чтобы перемещать камеру, используйте среднюю кнопку мыши и\nстрелочки"
    };
    private string[] ForSecond = new[]
    {
        "   Бургермен сталкивается с первыми препятствиями со стороны людей,\nкоторые хотят сохранить мир и процветание, достигнутые благодаря\nздоровому образу жизни. Он принимает решительные меры против\nоппонентов, чтобы продвинуть свою идеологию ожирения.",
        "  \"Те, кто осмеливаются противиться моему величию, будут уничтожены\nв вихре калорийного вкуса! Никто не остановит меня на пути к\nпобеде над здоровым питанием!\"",
        "                          Качок: \"У тебя не получится захватить власть в этом городе!\n                                          Спорт и сила - ты могила!\""
    };
    private string[] ForThird = new[]
{
        "   Бургермен утверждает свою власть и начинает систематически уничтожать\nвсех, кто остается верен здоровому образу жизни. Он предлагает\nлюдям выбор - принять идеологию ожирения или\nбыть уничтоженными.",
        "  \"ХАХАХАА, в мире почти не осталось фитнес-клубов и рестаранов здорового\nпитания! Уже почти все люди имеют 2ую степень ожирения!\"",
        "   Босс ЕГОР: \"Бургермен, твоя тирания ужасна, я всю жизнь жил по устоям,\nкоторые ты пропогандируешь, но сейчас я понял, что это всё\nнеправильно! Я не дам тебе захватить мир и обязательно\nначну заниматься спортом и следить за своим питанием!\""
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
        enemyPic = root.Q<VisualElement>("enemyPic");
        if (LevelNum == 3)
            enemyPic.style.backgroundImage = new StyleBackground(egorPic);
        var allLines = new[] { ForFirst, ForSecond, ForThird };

        dialogueLines = allLines[LevelNum - 1];



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
            if (tutor != null)
                Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("tutorial");
            else
                Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("hud");
            return;
        }
        if ((LevelNum == 2 || LevelNum == 3) && dialogueLineNum == 2)
        {
            enemyPic.style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
        }

        displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[dialogueLineNum]));
    }
}
