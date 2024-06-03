using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdateManager : MonoBehaviour
{
    [SerializeField] private GameObject Hud;
    private UI uiScript;
    private CameraScript cameraScript;
    private LevelTransition levelTransition;
    public bool isOnButton = false;

    private void Start()
    {
        levelTransition = GetComponent<LevelTransition>();
        uiScript = Hud.GetComponent<UI>();
        cameraScript = GetComponent<CameraScript>();

        levelTransition.ChangeDialogueScreen();
        isOnButton = true;
    }

    public void SetUpHud()
    {
        uiScript.SetMoneyText(cameraScript.money);
        uiScript.SetHealthBar(cameraScript.healthPoints);
        uiScript.SetEnemyLeft(cameraScript.enemyNum);
    }

    public void EnemyNumUpdate(int enemyNum)
    {
        uiScript.SetEnemyLeft(enemyNum);
    }

    public void HealthPointsUpdate(int healthPoints)
    {
        uiScript.SetHealthBar(healthPoints);
    }
    
    public void MoneyUpdate(int money)
    {
        uiScript.SetMoneyText(money);
    }

    public void ChangeScreen(string screenName)
    {
        switch (screenName)
        {
            case "gameover":
                levelTransition.ChangeGameOverScreen();
                break;
            case "victory":
                levelTransition.ChangeVictoryScreen();
                break;
            case "hud":
                levelTransition.ChangeHudScreen();
                SetUpHud();
                break;
            case "tutorial":
                levelTransition.ChangeTutorialScreen();
                break;
            case "unTutor":
                levelTransition.TurnOfTutorialScreen();
                break;
            case "win":
                levelTransition.ChangeWinScreen();
                break;
            case "final":
                levelTransition.ChangeFinalScene();
                break;
            default:
                Debug.Log("WRONG SCREEN TRANSITION NAME");
                break;
        }
    }
}
