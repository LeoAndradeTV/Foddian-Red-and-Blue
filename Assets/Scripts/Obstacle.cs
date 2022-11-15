using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.OnSwitchDoneMoving += SwitchVisibleObstacles;
    }

    private void OnDisable()
    {
        Actions.OnSwitchDoneMoving -= SwitchVisibleObstacles;
    }

    protected abstract void SwitchVisibleObstacles();
}
