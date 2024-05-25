using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    private UiUpdateManager uiUpdateManager;

    public GameObject tower;
    public int money = 200;
    public int cost = 100;
    public int healthPoints = 10;
    private bool dead = false;
    private readonly float upZoomBorder = 15;
    private readonly float downZoomBorder = 3;
    public int enemyNum = 1;

    [SerializeField]
    private Tilemap tilemap;

    public int LevelNum = 1; 

    [SerializeField]
    private float moveAmount = 2f;
    private Vector3 cameraMove;
    [SerializeField]
    private float scrollSpeed = 20f;

    [SerializeField]
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    private void Start()
    {
        uiUpdateManager = GetComponent<UiUpdateManager>();

        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    void Update()
    {
        CheckEdgeMovement();
        CheckZoom();
        if (Input.GetMouseButtonDown(0) && !uiUpdateManager.isOnButton)
        {
            SpawnOnClick();
        }

        if (healthPoints <= 0 && !dead)
        {
            CheckGameOver();
        }
        else
        {
            CheckVictory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = (Time.timeScale + 1) % 2;
        }

    }

    public void HandleEnemyDeath()
    {
        enemyNum--;
        uiUpdateManager.EnemyNumUpdate(enemyNum);
    }

    public void DealDamage(int damage)
    {
        healthPoints -= damage;
        uiUpdateManager.HealthPointsUpdate(healthPoints);
    }

    public void ChangeMoney(int moneyDelta)
    {
        money += moneyDelta;
        uiUpdateManager.MoneyUpdate(money);
    }

    private void CheckGameOver()
    {
        if (healthPoints <= 0)
        {
            dead = true;
            soundManager.PlayGameOver();
            uiUpdateManager.ChangeScreen("gameover");
        }
    }

    private void CheckVictory()
    {
        if (MapGlobalFields.allEnemies.Count == 0 && enemyNum == 0)
        {
            uiUpdateManager.ChangeScreen("victory");
        }
    }

    private void SpawnOnClick()
    {
        var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPos.z = 0f;

        var cellPos = tilemap.WorldToCell(clickPos);
        if (!PlaceOnGrid(cellPos))
            return;
        soundManager.PlayTowerSpawn();

        Instantiate(tower, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
        ChangeMoney(-cost);
    }

    private bool PlaceOnGrid(Vector3Int cellPos)
    {
        if (LevelNum == 1)
        {
            if (cellPos.y + 3 < 0 || cellPos.y + 3 > 4
           || cellPos.x + 5 < 0 || cellPos.x + 5 > 9
           || MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] == 1
           || money < cost)
                return false;
            MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] = 1;
            return true;
        }
        if (LevelNum == 2)
        {
            if (cellPos.y < -4 || cellPos.y > 1
           || cellPos.x < -8 || cellPos.x > 8
           || MapGlobalFields.lockedCell[cellPos.y + 4, cellPos.x + 8] == 1
           || money < cost)
                return false;
            MapGlobalFields.lockedCell[cellPos.y + 4, cellPos.x + 8] = 1;
            return true;
        }
        if (LevelNum == 3)
        {
            if (cellPos.y < -5 || cellPos.y > 0
                || cellPos.x < -8 || cellPos.x > 4
                || MapGlobalFields.lockedCell[cellPos.y + 5, cellPos.x + 8] == 1
                || money < cost)
                return false;
            MapGlobalFields.lockedCell[cellPos.y + 5, cellPos.x + 8] = 1;
            return true;
        }
        return false;

    }

    private void CheckEdgeMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow) &&  Camera.main.transform.position.x < 15)
            cameraMove.x = 1;
        if (Input.GetKey(KeyCode.LeftArrow) && Camera.main.transform.position.x > -15)
            cameraMove.x = -1;
        if (Input.GetKey(KeyCode.UpArrow) && Camera.main.transform.position.y < 13)
            cameraMove.y = 1;
        if (Input.GetKey(KeyCode.DownArrow) && Camera.main.transform.position.y > -13)
            cameraMove.y = -1;

        Camera.main.transform.position += cameraMove.normalized * moveAmount * Time.deltaTime;
        cameraMove = Vector3.zero;
    }

    private void CheckZoom()
    {
        var zoomDelta = Camera.main.orthographicSize - Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
        if (zoomDelta < upZoomBorder && zoomDelta > downZoomBorder)
            Camera.main.orthographicSize = zoomDelta;
    }
}
