using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    public string FirstScene;
    public GameObject sound;
    private AudioSource audioSource;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var startGameButton = root.Q<Button>("PlayButton");
        var dontStartGameButton = root.Q<Button>("DontPlayButton");
        audioSource = sound.GetComponent<AudioSource>();


        startGameButton.clicked += StartGameButton_clicked;
        dontStartGameButton.clicked += DontStartGameButton_clicked;
    }

    private void StartGameButton_clicked()
    {
        audioSource.Play();
        SceneManager.LoadScene(FirstScene);
    }
    private void DontStartGameButton_clicked()
    {
        audioSource.Play();
        Application.Quit();
    }
}
