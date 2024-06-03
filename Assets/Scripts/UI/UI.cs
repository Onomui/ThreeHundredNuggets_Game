using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject burgerTower;
    [SerializeField]
    private Sprite burgerTowerSprite;
    [SerializeField]
    private GameObject cakeTower;
    [SerializeField]
    private Sprite cakeTowerSprite;
    [SerializeField]
    private GameObject colaTower;
    [SerializeField]
    private Sprite colaTowerSprite;
    [SerializeField]
    private GameObject popcornTower;
    [SerializeField]
    private Sprite popcornTowerSprite;
    [SerializeField]
    private GameObject farmTower;
    [SerializeField]
    private Sprite farmTowerSprite;

    private CameraScript cameraScript;
    private Label moneyText;
    private VisualElement pic;
    private Label healthPoints;
    private Label enemyLeft;
    private ProgressBar healthBar;
    private Label description;

    private AudioSource clickedAudio;

    private UiUpdateManager uiUpdateManager;
    private void OnEnable()
    {
        uiUpdateManager = Camera.main.GetComponent<UiUpdateManager>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        var buttonBurger = root.Q<Button>("ButtonBurger");
        var buttonCola = root.Q<Button>("ButtonCola");
        var buttonCake = root.Q<Button>("ButtonCake");
        var buttonPopcorn = root.Q<Button>("ButtonPopcorn");
        var buttonFarm = root.Q<Button>("ButtonFarm");

        moneyText = root.Q<Label>("Money");
        pic = root.Q<VisualElement>("PicContainer");
        healthPoints = root.Q<Label>("HealthPoints");
        enemyLeft = root.Q<Label>("enemyLeft");
        healthBar = root.Q<ProgressBar>("healthBar");
        description = root.Q<Label>("Description");
        
        clickedAudio = GetComponent<AudioSource>();

        buttonBurger.clicked += ButtonBurger_clicked;
        buttonBurger.RegisterCallback<MouseEnterEvent>((type) => { ShowDescription("Стреляет одиночными сочными бургерами"); });
        buttonBurger.RegisterCallback<MouseLeaveEvent>((type) => { HideDesctiption(); });
        buttonCola.clicked += ButtonCola_clicked;
        buttonCola.RegisterCallback<MouseEnterEvent>((type) => { ShowDescription("Стреляет ледяной газировкой и замедляет врагов"); });
        buttonCola.RegisterCallback<MouseLeaveEvent>((type) => { HideDesctiption(); });
        buttonCake.clicked += ButtonCake_clicked;
        buttonCake.RegisterCallback<MouseEnterEvent>((type) => { ShowDescription("Стреляет тортом, который взрывается при попадании"); });
        buttonCake.RegisterCallback<MouseLeaveEvent>((type) => { HideDesctiption(); });
        buttonPopcorn.clicked += ButtonPopcorn_clicked;
        buttonPopcorn.RegisterCallback<MouseEnterEvent>((type) => { ShowDescription("Стреляет попкорном во все четыре стороны"); });
        buttonPopcorn.RegisterCallback<MouseLeaveEvent>((type) => { HideDesctiption(); });
        buttonFarm.clicked += ButtonFarm_clicked;
        buttonFarm.RegisterCallback<MouseEnterEvent>((type) => { ShowDescription("Фармит деньги (+25 каждые 5 секунд)"); });
        buttonFarm.RegisterCallback<MouseLeaveEvent>((type) => { HideDesctiption(); });
        healthBar.highValue = cameraScript.healthPoints;
        healthBar.style.backgroundColor = Color.blue;
    }



    private void ShowDescription(string text)
    {
        description.visible = true;
        description.text = text;
        uiUpdateManager.isOnButton = true;
    }

    private void HideDesctiption()
    {
        description.visible = false;
        uiUpdateManager.isOnButton = false;
    }

    private void ButtonFarm_clicked()
    {
        Button_clicked(farmTower, 50, farmTowerSprite);
    }

    private void ButtonPopcorn_clicked()
    {
        Button_clicked(popcornTower, 200, popcornTowerSprite);
    }

    private void ButtonBurger_clicked()
    {
        Button_clicked(burgerTower, 100, burgerTowerSprite);
    }

    private void ButtonCola_clicked()
    {
        Button_clicked(colaTower, 50, colaTowerSprite);
    }

    private void ButtonCake_clicked()
    {
        Button_clicked(cakeTower, 400, cakeTowerSprite);
    }

    private void Button_clicked(GameObject tower, int cost, Sprite sprite)
    {
        clickedAudio.Play();
        cameraScript.tower = tower;
        cameraScript.cost = cost;
        pic.style.backgroundImage = new StyleBackground(sprite);
        cameraScript.HowerTowerOnGrid();
    }

    public void DeselectTower()
    {
        cameraScript.tower = null;
        cameraScript.cost = 0;
        pic.style.backgroundImage = new StyleBackground();
    }

    public void SetMoneyText(int curMoney)
    {
        moneyText.text = curMoney.ToString();
    }
    public void SetEnemyLeft(int enemyLeftNum)
    {
        enemyLeft.text = enemyLeftNum.ToString();
    }

    public void SetHealthBar(int hp)
    {
        healthBar.title = hp.ToString();
        healthBar.value = hp;
    }
    
}
