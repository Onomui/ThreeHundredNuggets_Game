using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
public class FrozenTower : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float fireRate = 1f;
    private float currentCooldown;
    private bool isLocked;
    private Vector2 vectorToTarget;
    
    void Start()
    {
        currentCooldown = fireRate;
    }


    void Update()
    {
        LookAtNearestEnemy();
        if (isLocked)
            Shoot();
    }

    private void LookAtNearestEnemy()
    {
        vectorToTarget = FindNeatestEnemy();
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private Vector2 FindNeatestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        var vector = Vector2.zero;
        foreach (var enemy in MapGlobalFields.allEnemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < shortestDistance)
            {
                shortestDistance = Vector2.Distance(enemy.transform.position, transform.position);
                vector = enemy.transform.position - transform.position;
            }
        }
        isLocked = vector != Vector2.zero;
        return vector;
    }

    private void Shoot()
    {
        
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            Instantiate(bullet, transform.position + new Vector3(vectorToTarget.x, vectorToTarget.y, 0).normalized, transform.rotation);
            currentCooldown = fireRate;
        }
    }
}