using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.OnSwitchDoneMoving += SwitchVisibleObstacles;
    }

    private void OnDisable()
    {
        Actions.OnSwitchDoneMoving -= SwitchVisibleObstacles;
    }

    private void SwitchVisibleObstacles()
    {
        if (GameManager.Instance.currentState == SwitchStates.red && gameObject.CompareTag("Blue Obstacle"))
        {
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            
        } else if (GameManager.Instance.currentState == SwitchStates.blue && gameObject.CompareTag("Red Obstacle"))
        {
            if (transform.childCount > 0)
            {
                transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            GetComponent<MeshRenderer>().enabled = false;
            
        }
        else
        {
            transform.GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            

        }
    }
}
