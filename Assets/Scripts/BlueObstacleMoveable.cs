using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObstacleMoveable : BlueObstacle, IMoveable
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
}
