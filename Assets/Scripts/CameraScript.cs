using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Hud;
    private UI uiScript;
    public GameObject tower;
    private int money = 200;
    public int cost = 100;


    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private float moveAmount = 2f;
    private Vector3 cameraMove;
    [SerializeField]
    private float scrollSpeed = 20f;

    private void Start()
    {
        uiScript = Hud.GetComponent<UI>();
    }

    void Update()
    {
        CheckEdgeMovement();
        CheckZoom();
        if (Input.GetMouseButtonDown(0))
        {
            SpawnOnClick();
        }

    }

    private void SpawnOnClick()
    {
        var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPos.z = 0f;

        var cellPos = tilemap.WorldToCell(clickPos);
        if (cellPos.y + 3 < 0 || cellPos.y + 3 > 4
            || cellPos.x + 5 < 0 || cellPos.x + 5 > 9
            || MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] == 1
            || money < cost)
            return;
        MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] = 1;

        Instantiate(tower, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
        ChangeMoney(-cost);
    }

    public void ChangeMoney(int moneyDelta)
    {
        money += moneyDelta;
        uiScript.UpdateMoneyText(money);
    }

    private void CheckEdgeMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            cameraMove.x = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            cameraMove.x = -1;
        if (Input.GetKey(KeyCode.UpArrow))
            cameraMove.y = 1;
        if (Input.GetKey(KeyCode.DownArrow))
            cameraMove.y = -1;

        Camera.main.transform.position += cameraMove.normalized * moveAmount * Time.deltaTime;
        cameraMove = Vector3.zero;
    }

    private void CheckZoom()
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize - Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
    }
}
