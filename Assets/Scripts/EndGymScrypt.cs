using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EndGymScrypt : MonoBehaviour
{

    private CameraScript cameraScript;


    private void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            var enemyScript = collision.GetComponent<BasicEnemy>();
            cameraScript.DealDamage(enemyScript.Damage);
            enemyScript.KillEnemy();
        }
    }
}
