using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset = new Vector3(0f, 7f, -15.6f);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
