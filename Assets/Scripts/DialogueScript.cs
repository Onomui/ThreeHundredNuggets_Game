using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueScript : MonoBehaviour
{
    private string[] dialogueLines = new[]
    {
        "������ ��������� ������� ������� �������� ������ ����������, ���������� \n����������� ���������� ������� � ���������� �����. �� �������� ���� ������\n �� ��������������� �������� �������� ����� ��������� �����.",
        "\"����� � ���� ����� ������� ���� �����! ����� ���� �������� ���������� \n����� ��������, � � ���� ������� ���� � ������� ����� �������������� \n�����������! �����������, ������ ���� ���� �� ���� ��������!\""
    };
    private int dialogueLineNum = 0;
    private Label dialogueText;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        

        var dialogueButton = root.Q<Button>("dialogueButton");
        dialogueText = root.Q<Label>("dialogueText");
        
        dialogueText.text = dialogueLines[dialogueLineNum];

        dialogueButton.clicked += DialogueButton_clicked;
    }

    private void DialogueButton_clicked()
    {
        dialogueLineNum++;
        if (dialogueLineNum == dialogueLines.Length)
        {
            Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("hud");
            return;
        }
        dialogueText.text = dialogueLines[dialogueLineNum];
    }
}
