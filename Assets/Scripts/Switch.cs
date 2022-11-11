using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private SphereCollider interactionRadius;
    private SwitchStates state = SwitchStates.none;

    private void OnEnable()
    {
        Actions.OnSwitchPulled += ChangeSwitchState;
    }

    private void OnDisable()
    {
        Actions.OnSwitchPulled -= ChangeSwitchState;
    }

    private void ChangeSwitchState()
    {
        if (state == SwitchStates.red)
        {
            state = SwitchStates.blue;
            Debug.Log(state);
        } else
        {
            state = SwitchStates.red;
            Debug.Log(state);
        }
    }

}
