using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private Tilemap tilemap;

    private float edgeSize = 20f;
    [SerializeField]
    private float moveAmount = 2f;
    private Vector3 cameraMove;
    [SerializeField]
    private float scrollSpeed = 20f;
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
            || MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] == 1)
            return;

        MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] = 1;
        Instantiate(tower, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
    }

    private void CheckEdgeMovement()
    {
        if (Input.mousePosition.x > Screen.width - edgeSize)
            cameraMove.x = 1;
        if (Input.mousePosition.x < edgeSize)
            cameraMove.x = -1;
        if (Input.mousePosition.y > Screen.height - edgeSize)
            cameraMove.y = 1;
        if (Input.mousePosition.y < edgeSize)
            cameraMove.y = -1;

        Camera.main.transform.position += cameraMove.normalized * moveAmount * Time.deltaTime;
        cameraMove = Vector3.zero;
    }

    private void CheckZoom()
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize - Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
    }
}
