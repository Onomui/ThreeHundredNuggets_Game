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

    private CameraScript cameraScript;
    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();
        StartCoroutine(SpawnEnemy(basicEnemy, respawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float respawnTimer)
    {
        var newEnemy = Instantiate(enemy);
        cameraScript.enemyNum--;
        var newEnemyScript = newEnemy.GetComponent<BasicEnemy>();
        newEnemyScript.path = path;
        MapGlobalFields.allEnemies.Add(newEnemy);

        yield return new WaitForSeconds(respawnTimer);
        if (respawnTimer > spawnTimeLimit)
            respawnTimer -= spawnDecrease;
        if (cameraScript.enemyNum > 0)
            StartCoroutine(SpawnEnemy(ChooseEnemyRandomly(), respawnTimer));
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