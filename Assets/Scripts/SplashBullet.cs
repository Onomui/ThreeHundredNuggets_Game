using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0.5f;
    [SerializeField] private float bulletLifeTime = 3; 
    private CircleCollider2D collider;
    private bool IsStoped = false;
    private List<GameObject> injured;
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        injured = new();
    }

    void Update()
    {
        if (!IsStoped)
            transform.Translate(Vector3.up * bulletSpeed);
        CheckFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collider.radius = 30;
            StartCoroutine(DeleteBullet());
            IsStoped = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {  
        if (collision.tag == "Enemy" && !injured.Contains(collision.gameObject)) 
        {
                collision.GetComponent<BasicEnemy>().Health--;
                injured.Add(collision.gameObject);
        }
    }

    private IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    private void CheckFall()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }
}