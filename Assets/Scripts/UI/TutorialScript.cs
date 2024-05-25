using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Camera.main.GetComponent<UiUpdateManager>().ChangeScreen("unTutor");
    }
}
