using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
public class PopcornTower : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float fireRate = 1f;
    private float currentCooldown;
    void Start()
    {
        currentCooldown = fireRate;
    }


    void Update()
    {       
        Shoot();
    }

    private void Shoot()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(bullet, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
            Instantiate(bullet, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

            currentCooldown = fireRate;
        }
    }
}