using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialScript : MonoBehaviour
{
    public Sprite Health;
    public Sprite Enemies;
    public Sprite Money;
    public Sprite Towers;
    private VisualElement bg;
    private Sprite[] slides;

    private int i = 1;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        bg = root.Q<VisualElement>("Bg");
        slides = new Sprite[] {Health, Enemies, Money, Towers};
    }
    private void OnMouseDown()
    {
        if (i <= 3)
        {
            bg.style.backgroundImage = new StyleBackground(slides[i]);
            i++;
        }
        else
        {
            Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("unTutor");
        }
    }
}
