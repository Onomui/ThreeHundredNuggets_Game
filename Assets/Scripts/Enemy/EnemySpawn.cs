using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject basicEnemy;
    [SerializeField]
    private GameObject speedEnemy;
    [SerializeField]
    private GameObject bossEnemy;
    [SerializeField]
    private GameObject EGOR;
    
    private Stack<GameObject> enemyStack = new();

    [SerializeField]
    private Transform[] path;
    [SerializeField]
    private Transform[] secondPath;

    private int pathNum = 0;
    
    [SerializeField]
    private float respawnTimer;
    [SerializeField]
    private float spawnDecrease = 0.1f;
    [SerializeField]
    private float spawnTimeLimit = 0.5f;
    [SerializeField]
    private float gameSpeed = 1;

    private int levelNum;

    private int enemyNumToSpawn;
    void Start()
    {
        enemyNumToSpawn = Camera.main.GetComponent<CameraScript>().enemyNum;
        levelNum = Camera.main.GetComponent<CameraScript>().LevelNum;
        SetUpEnemyStack();
        StartCoroutine(SpawnEnemy(basicEnemy));
    }

    private IEnumerator SpawnEnemy(GameObject enemy)
    {
        var newEnemy = Instantiate(enemy);
        enemyNumToSpawn--;
        var newEnemyScript = newEnemy.GetComponent<BasicEnemy>();
        newEnemyScript.path = ChoosePath();
        MapGlobalFields.allEnemies.Add(newEnemy);

        yield return new WaitForSeconds(respawnTimer);
        Debug.Log(respawnTimer);
        if (respawnTimer >= spawnTimeLimit)
            StartCoroutine(SpawnRateUp());

        if (enemyNumToSpawn > 0)
            StartCoroutine(SpawnEnemy(ChooseFromStack()));
    }

    private IEnumerator SpawnRateUp()
    {
        yield return new WaitForSeconds(gameSpeed);
        respawnTimer -= spawnDecrease;
    }

    private Transform[] ChoosePath()
    {
        if (secondPath.Length == 0)
            return path;
        pathNum = (pathNum + 1) % 2;

        if (pathNum == 1)
            return secondPath;
        return path;
    }


    private GameObject ChooseFromStack()
    {
        return enemyStack.Pop();
    }

    private void SetUpEnemyStack()
    {
        if (levelNum == 1)
        {
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
        }

        if (levelNum == 2)
        {
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
        }

        if (levelNum == 3)
        {
            enemyStack.Push(EGOR);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(EGOR);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(EGOR);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(EGOR);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(EGOR);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(EGOR);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(bossEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
            enemyStack.Push(basicEnemy);
            enemyStack.Push(speedEnemy);
        }
    }
}