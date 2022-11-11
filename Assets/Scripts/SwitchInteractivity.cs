using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractivity : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.switchIsInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.switchIsInteractable = false;
    }

}
