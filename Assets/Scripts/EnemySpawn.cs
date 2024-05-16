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
    private GameObject superBossEnemy;
    [SerializeField]
    private Transform[] path;
    [SerializeField]
    private float respawnTimer;
    [SerializeField]
    private float spawnDecrease = 0.1f;
    [SerializeField]
    private float spawnTimeLimit = 0.5f;
    [SerializeField]
    private float gameSpeed = 1;

    private int enemyNumToSpawn;
    void Start()
    {
        enemyNumToSpawn = Camera.main.GetComponent<CameraScript>().enemyNum;
        StartCoroutine(SpawnEnemy(basicEnemy, respawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float respawnTimer)
    {
        var newEnemy = Instantiate(enemy);
        enemyNumToSpawn--;
        var newEnemyScript = newEnemy.GetComponent<BasicEnemy>();
        newEnemyScript.path = path;
        MapGlobalFields.allEnemies.Add(newEnemy);

        yield return new WaitForSeconds(respawnTimer);

        if (respawnTimer >= spawnTimeLimit)
            StartCoroutine(SpawnRateUp());

        if (enemyNumToSpawn > 0)
            StartCoroutine(SpawnEnemy(ChooseEnemyRandomly(), respawnTimer));
    }

    private IEnumerator SpawnRateUp()
    {
        yield return new WaitForSeconds(gameSpeed);
        respawnTimer -= spawnDecrease;
    }

    private GameObject ChooseEnemyRandomly()
    {
        var i = Random.Range(0, 101);
        switch (i)
        {
            case < 50:
                return basicEnemy;
            case < 90:
                return speedEnemy;
            case 100:
                return superBossEnemy;
            default: return bossEnemy;
        }
    }
}