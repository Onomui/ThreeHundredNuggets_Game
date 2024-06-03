using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public GameObject redScreen;

    private UiUpdateManager uiUpdateManager;
    private Vector3 initialPosition;
    private DialogueScript dialogueScript;
    public GameObject tower;
    public int money = 200;
    public int cost = 100;
    public int healthPoints = 10;
    private bool dead = false;
    private readonly float upZoomBorder = 15;
    private readonly float downZoomBorder = 3;
    public int enemyNum = 1;

    [SerializeField] private Tilemap tilemap;

    public int LevelNum = 1;

    [SerializeField] private float moveAmount = 2f;
    private Vector3 cameraMove;
    [SerializeField] private float scrollSpeed = 20f;

    [SerializeField] private GameObject soundManagerObject;
    private SoundManager soundManager;

    public GameObject Ui;
    public GameObject Tutor;
    private UI uiScript;
    private GameObject alphaTower;
    private Vector3 mousePos;
    private bool bgMusicIsPlaying = true;

    public GameObject PauseMenu;

    private void Start()
    {
        uiScript = Ui.GetComponent<UI>();
        uiUpdateManager = GetComponent<UiUpdateManager>();
        
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

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
        if (Input.GetKeyDown(KeyCode.Escape) && Ui.activeSelf && (Tutor == null || !Tutor.activeSelf))
        {
            if (!PauseMenu.activeSelf)
                Pause();
            else
            {
                Unpause();
            }
        }

        if (alphaTower != null)
        {
            var cellPos = tilemap.WorldToCell(mousePos);
            if (PlaceOnGrid(cellPos, false))
                alphaTower.transform.position = tilemap.GetCellCenterWorld(cellPos);
        }


    }

    public void Mute()
    {
        if (bgMusicIsPlaying)
        {
            soundManager.bgMusic.volume = 0f;
            bgMusicIsPlaying = false;
        }
        else
        {
            soundManager.bgMusic.volume = 0.02f;
            bgMusicIsPlaying = true;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
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
        OnHit();
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
        if (MapGlobalFields.allEnemies.Count == 0 && enemyNum == 0 && LevelNum != 3)
        {
            uiUpdateManager.ChangeScreen("victory");
        }
        
        if (LevelNum == 3 && MapGlobalFields.allEnemies.Count == 0 && enemyNum == 0)
            uiUpdateManager.ChangeScreen("final");
    }

    private void SpawnOnClick()
    {
        if (tower == null)
            return;
        var clickPos = mousePos;

        var cellPos = tilemap.WorldToCell(clickPos);
        if (!PlaceOnGrid(cellPos, true))
        {
            GetComponent<ErrorBuildPopUp>().SpawnAndMovePopup(clickPos);
            return;
        }
        soundManager.PlayTowerSpawn();
        UnHower();
        Instantiate(tower, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
        ChangeMoney(-cost);
        GetComponent<TowerBuildPopup>().SpawnAndMovePopup(clickPos);
        uiScript.DeselectTower();
    }

    public void HowerTowerOnGrid()
    {
        UnHower();
        alphaTower = Instantiate(tower, new Vector3(100, 100, 0), Quaternion.identity);
        alphaTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        if (alphaTower.GetComponent<BasicTower>()!= null)
            alphaTower.GetComponent<BasicTower>().enabled = false;
        if (alphaTower.GetComponent<SplashTower>() != null)
            alphaTower.GetComponent<SplashTower>().enabled = false;
        if (alphaTower.GetComponent<FarmTower>() != null)
            alphaTower.GetComponent<FarmTower>().enabled = false;
        if (alphaTower.GetComponent<PopcornTower>() != null)
            alphaTower.GetComponent<PopcornTower>().enabled = false;
    }

    private void UnHower()
    {
        if (alphaTower != null)
            Destroy(alphaTower);
    }

    private bool PlaceOnGrid(Vector3Int cellPos, bool isLocking)
    {
        if (LevelNum == 1)
        {
            if (cellPos.y + 3 < 0 || cellPos.y + 3 > 4
           || cellPos.x + 5 < 0 || cellPos.x + 5 > 9
           || MapGlobalFields.lockedCell[cellPos.y + 3, cellPos.x + 5] == 1
           || money < cost)
                return false;
            if (isLocking)
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
            if (isLocking)
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
            if (isLocking)
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

    public void OnHit()
    {
        redScreen.GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
        var alpha = 0.3f;
        StartCoroutine(Fade(alpha));

        initialPosition = transform.position;
        InvokeRepeating("DoShake", 0, 0.1f);
        Invoke("StopShake", 0.5f);
    }

    private IEnumerator Fade(float alpha)
    {
        alpha -= 0.05f;
        yield return new WaitForSeconds(0.1f);
        redScreen.GetComponent<Image>().color = new Color(1, 0, 0, alpha);
        if (alpha > 0)
            StartCoroutine(Fade(alpha));
    }

    void DoShake()
    {
        float offsetX = UnityEngine.Random.Range(-0.1f, 0.1f);
        float offsetY = UnityEngine.Random.Range(-0.1f, 0.1f);
        transform.position = new Vector3(initialPosition.x + offsetX, initialPosition.y + offsetY, initialPosition.z);
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        transform.position = initialPosition;
    }
}
