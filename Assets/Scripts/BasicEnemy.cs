using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform[] path;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Sprite frozenEnemy;
    private SpriteRenderer spriteRenderer;
    private int curPathPoint = 1;
    private bool isStopped = false;
    private float stopDuration = 0f;
    private float stopTimer = 0f;
    private Animator animator;

    public int Health = 3;
    void Start()
    {
        transform.position = path[0].position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (isStopped)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopDuration)
            {
                isStopped = false;
                animator.enabled = true;
            }
        }

        if (!isStopped)
        {
            MoveOnPath();
        }
        CheckDeath();
    }

    public void StopMovement(float duration)
    {
        isStopped = true;
        stopDuration = duration;
        stopTimer = 0f;
    }

    public void ChangeSprite()
    {
        if (frozenEnemy != null)
        {
            animator.enabled = false;
            spriteRenderer.sprite = frozenEnemy;
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

    private void CheckDeath()
    {
        if (Health <= 0)
        {
            MapGlobalFields.allEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

}