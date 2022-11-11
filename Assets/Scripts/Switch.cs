using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private SphereCollider interactionRadius;

    public bool isInteractable;

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
        if (isInteractable)
        {
            if (GameManager.Instance.currentState == SwitchStates.red)
            {
                GameManager.Instance.currentState = SwitchStates.blue;
                Debug.Log(GameManager.Instance.currentState);
            }
            else
            {
                GameManager.Instance.currentState = SwitchStates.red;
                Debug.Log(GameManager.Instance.currentState);
            }
        }
    }

}
