using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0.5f;
    [SerializeField] private float bulletLifeTime = 3;
    [SerializeField] private float splashRadius = 30;
    [SerializeField] private Sprite explosion;
    [SerializeField] private int damage = 2;
    private CircleCollider2D circleCollider;
    private bool IsStoped = false;
    private List<GameObject> injured;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        injured = new();
    }

    void FixedUpdate()
    {
        if (!IsStoped)
            transform.Translate(Vector3.up * bulletSpeed);
        CheckFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            circleCollider.radius = splashRadius;
            transform.eulerAngles = Vector3.zero;
            GetComponent<SpriteRenderer>().sprite = explosion;
            StartCoroutine(DeleteBullet());
            IsStoped = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {  
        if (collision.tag == "Enemy" && !injured.Contains(collision.gameObject)) 
        {
                collision.GetComponent<BasicEnemy>().Health -= damage;
                injured.Add(collision.gameObject);
        }
    }

    private IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void CheckFall()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
            Destroy(gameObject);
    }
}