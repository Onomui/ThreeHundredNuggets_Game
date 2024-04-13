using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private List<GameObject> enemies;
    void Start()
    {
        enemies = enemySpawner.GetComponent<EnemySpawn>().allEnemies;
    }


    void Update()
    {
        LookAtNearestEnemy();
    }

    private void LookAtNearestEnemy()
    {
        var vectorToTarget = FindNeatestEnemy();
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private Vector2 FindNeatestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        var vector = Vector2.zero;
        foreach(var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < shortestDistance)
            {
                shortestDistance = Vector2.Distance(enemy.transform.position, transform.position);
                vector = enemy.transform.position - transform.position;
            }
        }
        return vector;
    }
}
