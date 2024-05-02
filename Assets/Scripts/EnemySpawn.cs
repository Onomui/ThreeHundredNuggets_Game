using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform[] path;
    [SerializeField]
    private float respawnTimer;
    [SerializeField]
    private float spawnDecrease = 0.1f;

    private CameraScript cameraScript;

    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();
        StartCoroutine(SpawnEnemy(enemy, respawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float respawnTimer)
    {
        var newEnemy = Instantiate(enemy);
        cameraScript.enemyNum--;
        var newEnemyScript = newEnemy.GetComponent<BasicEnemy>();
        newEnemyScript.path = path;
        MapGlobalFields.allEnemies.Add(newEnemy);

        yield return new WaitForSeconds(respawnTimer);
        if (respawnTimer > 0.5f)
            respawnTimer -= spawnDecrease;
        if (cameraScript.enemyNum > 0)
            StartCoroutine(SpawnEnemy(enemy, respawnTimer));
    }
}