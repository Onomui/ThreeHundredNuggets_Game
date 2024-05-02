using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class VictoryScreenScript : MonoBehaviour
{
    public string nextSceneName = "SecondScene";
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var restartButton = root.Q<Button>("nextLevel");
        restartButton.clicked += NextLevel_clicked;
    }

    private void NextLevel_clicked()
    {
        LoadScene();
        Time.timeScale = 1;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
