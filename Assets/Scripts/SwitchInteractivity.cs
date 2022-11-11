using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractivity : MonoBehaviour
{
    [SerializeField] private Switch thisSwitch;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           thisSwitch.isInteractable = true;
           GameManager.Instance.switchIsInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        thisSwitch.isInteractable = false;
        GameManager.Instance.switchIsInteractable = false;
    }

}
