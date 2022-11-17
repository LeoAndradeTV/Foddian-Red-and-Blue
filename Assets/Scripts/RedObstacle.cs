using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacle : Obstacle
{
    protected override void SwitchVisibleObstacles()
    {
        if (GameManager.Instance.currentState == SwitchStates.blue)
        {
            GetComponent<MeshRenderer>().enabled = false;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
        } else
        {
            GetComponent<MeshRenderer>().enabled = true;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
