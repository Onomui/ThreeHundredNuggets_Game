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
    public GameObject GameOverScreen;
    private gameOverScript gameoverScript;

    private void OnEnable()
    {
        {
            gameoverScript = GameOverScreen.GetComponent<gameOverScript>();

            var root = GetComponent<UIDocument>().rootVisualElement;
            
            var continueButton = root.Q<Button>("Continue");
            var soundButton = root.Q<Button>("Sound");
            var restartButton = root.Q<Button>("Restart");
            var mainMenuButton = root.Q<Button>("MainMenu");
            var exitButton = root.Q<Button>("Exit");

            clickedAudio = GetComponent<AudioSource>();

            continueButton.clicked += ContinueButton_clicked;
            soundButton.clicked += SoundButton_clicked;
            restartButton.clicked += RestartButton_clicked;
            mainMenuButton.clicked += MainMenuButton_clicked;
            exitButton.clicked += ExitButton_clicked;

        }

    }

    private void RestartButton_clicked()
    {
        clickedAudio.Play();
        gameoverScript.RestartButton_clicked();
    }
    private void ExitButton_clicked()
    {
        clickedAudio.Play();
        Application.Quit();
    }

    private void MainMenuButton_clicked()
    {
        clickedAudio.Play();
        SceneManager.LoadScene("MainMenu");

    }

    private void SoundButton_clicked()
    {
        clickedAudio.Play();
        Camera.main.GetComponent<CameraScript>().Mute();
    }

    private void ContinueButton_clicked()
    {
        clickedAudio.Play();
        Camera.main.GetComponent<CameraScript>().Unpause();
    }
}
