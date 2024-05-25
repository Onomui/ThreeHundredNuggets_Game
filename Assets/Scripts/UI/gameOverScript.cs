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

        var restartButton = root.Q<Button>("restartButton");
        restartButton.clicked += RestartButton_clicked;
    }

    private void RestartButton_clicked()
    {
        LoadScene();
        Time.timeScale = 1;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(curSceneName);
    }
}
