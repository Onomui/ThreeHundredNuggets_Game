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
        moneyText = root.Q<Label>("Money");
        pic = root.Q<VisualElement>("PicContainer");
        healthPoints = root.Q<Label>("HealthPoints");
        enemyLeft = root.Q<Label>("enemyLeft");

        buttonBurger.clicked += ButtonBurger_clicked;
        buttonCola.clicked += ButtonCola_clicked;
        buttonCake.clicked += ButtonCake_clicked;
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
        Button_clicked(cakeTower, 150, cakeTowerSprite);
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
