using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerBuildPopup : MonoBehaviour
{
    public GameObject Popup;

    public void SpawnAndMovePopup(Vector3 spawnPos)
    {
        var popup = Instantiate(Popup, spawnPos, Quaternion.identity);
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().text = $"- {GetComponent<CameraScript>().cost}";
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 0);
        popup.GetComponent<PopupMove>().Move();
    }
}
