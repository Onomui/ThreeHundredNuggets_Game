using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMoveAndDestroy : MonoBehaviour
{
    public float speed = 1f;

    public void Move()
    {
        StartCoroutine(CoroutineMove());
    }

    public IEnumerator CoroutineMove()
    {
        var startPos = transform.position;
        var endPos = new Vector3(startPos.x, startPos.y + 2, startPos.z);
        var dist = Vector3.Distance(startPos, endPos);
        var startTime = Time.time;
        var canvasGroup = GetComponent<CanvasGroup>();
        while ((Time.time - startTime) * speed < dist)
        {
            canvasGroup.alpha -= 0.005f;
            float distCovered = (Time.time - startTime) * speed;
            float curDist = distCovered / dist;

            transform.position = Vector3.Lerp(startPos, endPos, curDist);

            yield return null;
        }

        Destroy(gameObject);
    }
}
