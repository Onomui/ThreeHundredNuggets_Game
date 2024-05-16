using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    [SerializeField]
    private float bulletLifeTime = 3;
    [SerializeField]
    private float freezeTime = 2f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed);
        CheckFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.tag == "Enemy")
        {
            var enemy = collision.GetComponent<BasicEnemy>();
            enemy.Freeze(freezeTime);
            Destroy(gameObject);
        }
    }
    
    private void CheckFall()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }

}