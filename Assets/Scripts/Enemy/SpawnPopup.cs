using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPopup : MonoBehaviour
{
    public GameObject Popup;
    public float speed = 1;

    public void SpawnAndMovePopup()
    {
        var spawnPos = transform.position;
        spawnPos.y += 1;
        var popup = Instantiate(Popup, spawnPos, Quaternion.identity);
        popup.transform.Find("PopupText").GetComponent<TextMeshProUGUI>().text = $"+ {GetComponent<BasicEnemy>().moneyDrop}";
        popup.GetComponent<PopupMoveAndDestroy>().Move();
    }
}
