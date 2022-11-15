using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacleBouncy : RedObstacle
{
    [SerializeField] private Rigidbody playerRb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb.AddForce(Vector3.up * 100f);
        }
    }
}
