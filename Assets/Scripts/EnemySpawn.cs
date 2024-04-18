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
    public List<GameObject> allEnemies;
    void Start()
    {
        StartCoroutine(SpawnEnemy(enemy, respawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float respawnTimer)
    {
        var newEnemy = Instantiate(enemy);
        var newEnemyScript = newEnemy.GetComponent<BasicEnemy>();
        newEnemyScript.path = path;
        newEnemyScript.enemySpawner = gameObject;
        allEnemies.Add(newEnemy);

        yield return new WaitForSeconds(respawnTimer);
        StartCoroutine(SpawnEnemy(enemy, respawnTimer));
    }
}