using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    [SerializeField]
    private float bulletLifeTime = 3;
    [SerializeField]
    private int damage = 1;
    void Start()
    {

    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed);
        CheckFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<BasicEnemy>().Health -= damage;
        }
        Destroy(gameObject);
    }
    
    private void CheckFall()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }

}