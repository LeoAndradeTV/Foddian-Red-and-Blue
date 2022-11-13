using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Vector3 startPos, endPos, currentPos;

    private void OnEnable()
    {
        startPos = transform.position;
        currentPos = startPos;
        endPos = startPos + Vector3.up * 20f;
        Actions.OnSwitchDoneMoving += OpenGate;
    }

    private void OnDisable()
    {
        Actions.OnSwitchDoneMoving -= OpenGate;
    }

    private void OpenGate()
    {
        StartCoroutine(GateMovement());
    }

    private IEnumerator GateMovement()
    {
        while (currentPos.y < endPos.y)
        {
            transform.Translate(Vector3.up / 10f);
            currentPos = transform.position;
            yield return null;
        }
        Destroy(gameObject);
    }
    
}
