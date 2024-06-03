using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerPopup : MonoBehaviour
{
    public GameObject Popup;

    public void SpawnAndMovePopup()
    {
        var spawnPos = transform.position;
        spawnPos.y += 1;
        var popup = Instantiate(Popup, spawnPos, Quaternion.identity);
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().text = $"+ 25";
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().color = new Color(0.1497006f, 1, 0);
        popup.GetComponent<PopupMove>().Move();
    }
}
