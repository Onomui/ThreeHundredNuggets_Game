using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    [SerializeField]
    private float bulletLifeTime = 3;
    void Start()
    {

    }
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed);
        CheckFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<BasicEnemy>().Health--;
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