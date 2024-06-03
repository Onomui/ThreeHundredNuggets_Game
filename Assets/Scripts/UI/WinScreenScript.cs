using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class WinScreenScript : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var continueButton = root.Q<Button>("continue");
        continueButton.clicked += ContinueButton_clicked;
    }

    private void ContinueButton_clicked()
    {
        Camera.main.GetComponent<LevelTransition>().ChangeFinalScene();
    }
}
