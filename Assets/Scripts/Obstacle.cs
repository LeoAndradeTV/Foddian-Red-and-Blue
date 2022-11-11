using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.OnSwitchPulled += SwitchVisibleObstacles;
    }

    private void OnDisable()
    {
        Actions.OnSwitchPulled -= SwitchVisibleObstacles;
    }

    private void SwitchVisibleObstacles()
    {
        if (GameManager.Instance.currentState == SwitchStates.red && gameObject.CompareTag("Blue Obstacle"))
        {
            GetComponent<MeshRenderer>().enabled = false;
        } else if (GameManager.Instance.currentState == SwitchStates.blue && gameObject.CompareTag("Red Obstacle"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            
        } else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
