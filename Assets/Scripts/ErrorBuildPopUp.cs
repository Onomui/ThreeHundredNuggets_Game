using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorBuildPopUp : MonoBehaviour
{
    public GameObject Popup;

    public void SpawnAndMovePopup(Vector3 spawnPos)
    {
        var popup = Instantiate(Popup, spawnPos, Quaternion.identity);
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().text = $"ме онксвхкняэ\nонярпнхрэ";
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().fontSize = 0.3f;
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().color = new Color(1f, 0, 0);
        popup.GetComponent<PopupMove>().Move();
    }
}
