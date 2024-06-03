using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private GameObject DialogueScreen;
    [SerializeField] private GameObject Hud;
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject TutorialScreen;
    [SerializeField] private GameObject FinalScreen;
    
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
   
    public void ChangeTutorialScreen()
    {
        Time.timeScale = 0;
        DialogueScreen.SetActive(false);
        Hud.SetActive(true);
        TutorialScreen.SetActive(true);
    }

    public void TurnOfTutorialScreen()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeHudScreen()
    {
        Time.timeScale = 1;
        DialogueScreen.SetActive(false);
        Hud.SetActive(true);
    }

    public void ChangeFinalScene()
    {
        Time.timeScale = 0f;
        Hud.SetActive(false);
        FinalScreen.SetActive(true);
    }
}
