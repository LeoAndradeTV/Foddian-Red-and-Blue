using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 startPosition;
    private Vector3 offset;

    private void Start()
    {
        startPosition = transform.position;
        offset = new Vector3(0f, 7f, startPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
