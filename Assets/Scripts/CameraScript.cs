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
    [SerializeField]
    private GameObject GameOverScreen;
    [SerializeField]
    private GameObject VictoryScreen;
    

    public GameObject tower;
    private int money = 200;
    public int cost = 100;
    public int healthPoints = 10;
    private bool dead = false;

    public int enemyNum;
    public int enemyNumForFirstScene = 10;

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
        enemyNum = enemyNumForFirstScene;

        uiScript = Hud.GetComponent<UI>();
        uiScript.SetMoneyText(money);
        uiScript.SetHealthPoints(healthPoints);
        uiScript.SetEnemyLeft(enemyNum);

        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    void Update()
    {
        uiScript.SetEnemyLeft(enemyNum);
        CheckEdgeMovement();
        CheckZoom();
        if (Input.GetMouseButtonDown(0))
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

    public void DealDamage(int damage)
    {
        healthPoints -= damage;
        uiScript.SetHealthPoints(healthPoints);
    }

    private void CheckGameOver()
    {
        if (healthPoints <= 0)
        {
            dead = true;
            soundManager.PlayGameOver();
            Time.timeScale = 0;
            Hud.SetActive(false);
            GameOverScreen.SetActive(true);
        }
    }

    private void CheckVictory()
    {
        if (MapGlobalFields.allEnemies.Count == 0 && enemyNum == 0)
        {
            Time.timeScale = 0;
            Hud.SetActive(false);
            VictoryScreen.SetActive(true);
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

    public void ChangeMoney(int moneyDelta)
    {
        money += moneyDelta;
        uiScript.SetMoneyText(money);
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
