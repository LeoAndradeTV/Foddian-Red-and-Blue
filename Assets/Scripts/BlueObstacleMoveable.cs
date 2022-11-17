using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObstacleMoveable : BlueObstacle, IMoveable
{
    private PlayerMovement player;


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
        player = GameObject.FindObjectOfType<PlayerMovement>();
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
            player.isGroundedRange = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
            player.isGroundedRange = 0.15f;
        }
    }
}
