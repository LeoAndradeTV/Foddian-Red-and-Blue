using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacleMoveable : RedObstacle, IMoveable
{
    private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    private IEnumerator endPosition;

    public void Move()
    {
        endPosition = MoveObstacle(endPos);
        StartCoroutine(endPosition);
    }

    private void Awake()
    {
        startPos = transform.position;
        Move();
    }

    private IEnumerator MoveObstacle(Vector3 endPos)
    {
        while (true)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Mathf.Sin(Time.time) + 1) / 2);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
