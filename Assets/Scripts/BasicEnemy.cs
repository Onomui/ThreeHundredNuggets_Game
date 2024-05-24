using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform[] path;
    [SerializeField]
    private float speed;
    private bool freezed = false;
    [SerializeField]
    private int moneyDrop = 10;
    private SpriteRenderer spriteRenderer;
    private int curPathPoint = 1;
    private bool isStopped = false;

    public int Health = 3;
    public int Damage = 1;

    private Animator anim;
    public string animName;
    private bool isAlive = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        transform.position = path[0].position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        CheckDeath();
    }
    private void FixedUpdate()
    {
        if (!isStopped)
        {
            MoveOnPath();
        }
    }

    private void MoveOnPath()
    {
        if (curPathPoint == path.Length)
            isStopped = true;
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, path[curPathPoint].position, speed * Time.deltaTime);
            if (transform.position == path[curPathPoint].position)
                curPathPoint++;
        }
    }

    public void DoDamage(int damage)
    {
        Health -= damage;
        if (!freezed && Health >= 1)
        {
            spriteRenderer.color = Color.red;
            StartCoroutine(RestoreColor());
        }
    }

    private IEnumerator RestoreColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public void Freeze(float duration)
    {
        freezed = true;
        speed /= 2;
        spriteRenderer.color =new Color(0.38f, 0.75f, 0.9f);
        StartCoroutine(RestoreSpeed(duration));
    }

    private IEnumerator RestoreSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        freezed = false;
        speed *= 2;
        spriteRenderer.color = Color.white;
    }

    private void CheckDeath()
    {
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            Camera.main.GetComponent<CameraScript>().ChangeMoney(moneyDrop);
            StartCoroutine(KillEnemy());
        }
    }

    public IEnumerator KillEnemy()
    {
        Destroy(GetComponent<CircleCollider2D>());
        MapGlobalFields.allEnemies.Remove(gameObject);
        Camera.main.GetComponent<CameraScript>().HandleEnemyDeath();
        isStopped = true;
        anim.Play($"Base Layer.{animName}", 0);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}