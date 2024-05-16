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

    public int enemyNum = 1;

    [SerializeField]
    private Tilemap tilemap;

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
        if (cellPos.y + 3 < 0 || cellPos.y + 3 > 4
            || cellPos.x + 5 < 0 || cellPos.x + 5 > 9
            || MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] == 1
            || money < cost)
            return;
        MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] = 1;

        soundManager.PlayTowerSpawn();

        Instantiate(tower, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
        ChangeMoney(-cost);
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
