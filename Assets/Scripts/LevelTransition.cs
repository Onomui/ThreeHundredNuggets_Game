using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private GameObject DialogueScreen;
    [SerializeField] private GameObject Hud;
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private GameObject GameOverScreen;

    public void AnimateText()
    {
    }
    public void ChangeDialogueScreen()
    {
        DialogueScreen.SetActive(true);
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
        DialogueScreen.SetActive(false);
        Hud.SetActive(true);
    }
}
