using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public string curSceneName = "FirstScene";
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        var mainMenuButton = root.Q<Button>("mainMenuButton");
        var restartButton = root.Q<Button>("restartButton");
        
        restartButton.clicked += RestartButton_clicked;
        mainMenuButton.clicked += MainMenu_clicked;
    }

    private void RestartButton_clicked()
    {
        LoadScene();
        Time.timeScale = 1;
    }

    private void MainMenu_clicked()
    {
        LoadMainMenu();
        Time.timeScale = 1;
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(curSceneName);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
