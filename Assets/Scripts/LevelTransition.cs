using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private GameObject DialogeScreen;
    [SerializeField] private GameObject Hud;
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private GameObject GameOverScreen;
    public void ChangeDialogueScreen()
    {
        DialogeScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void ChangeVictoryScreen()
    {
        Time.timeScale = 0;
        Hud.SetActive(false);
        VictoryScreen.SetActive(true);
    }

    public void ChangeGameOverScreen()
    {
        Time.timeScale = 0;
        Hud.SetActive(false);
        GameOverScreen.SetActive(true);
    }
    public void ChangeHudScreen()
    {
        Time.timeScale = 1;
        DialogeScreen.SetActive(false);
        Hud.SetActive(true);
    }
}
