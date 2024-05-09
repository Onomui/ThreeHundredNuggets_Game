using System;
using System.Collections;
using System.Collections.Generic;
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
    private void OnEnable()
    {
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

        buttonBurger.clicked += ButtonBurger_clicked;
        
        buttonCola.clicked += ButtonCola_clicked;
        buttonCake.clicked += ButtonCake_clicked;
        buttonPopcorn.clicked += ButtonPopcorn_clicked;
        buttonFarm.clicked += ButtonFarm_clicked;
    }

    private void ButtonFarm_clicked()
    {
        Button_clicked(farmTower, 100, farmTowerSprite);
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
        Button_clicked(cakeTower, 300, cakeTowerSprite);
    }

    private void Button_clicked(GameObject tower, int cost, Sprite sprite)
    {
        cameraScript.tower = tower;
        cameraScript.cost = cost;
        pic.style.backgroundImage = new StyleBackground(sprite);
    }
    public void SetMoneyText(int curMoney)
    {
        moneyText.text = curMoney.ToString();
    }
    public void SetHealthPoints(int curHealth)
    {
        healthPoints.text = curHealth.ToString();
    }
    public void SetEnemyLeft(int enemyLeftNum)
    {
        enemyLeft.text = enemyLeftNum.ToString();
    }
}
