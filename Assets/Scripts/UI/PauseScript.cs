using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseScript : MonoBehaviour
{

    private AudioSource clickedAudio;

    private void Awake()
    {
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var continueButton = root.Q<Button>("Continue");
            var soundButton = root.Q<Button>("Sound");
            var mainMenuButton = root.Q<Button>("MainMenu");
            var exitButton = root.Q<Button>("Exit");

            clickedAudio = GetComponent<AudioSource>();

            continueButton.clicked += ContinueButton_clicked;
            soundButton.clicked += SoundButton_clicked;
            mainMenuButton.clicked += MainMenuButton_clicked;
            exitButton.clicked += ExitButton_clicked;
        }
        
}

    private void ExitButton_clicked()
    {
        Application.Quit();
    }

    private void MainMenuButton_clicked()
    {
        SceneManager.LoadScene("MainMenu");

    }

    private void SoundButton_clicked()
    {
        Camera.main.GetComponent<CameraScript>().Mute();
    }

    private void ContinueButton_clicked()
    {
        Camera.main.GetComponent<CameraScript>().Unpause();
    }
}
