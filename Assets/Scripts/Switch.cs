using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private SphereCollider interactionRadius;

    private Animator animator;

    public bool isInteractable;
    public SwitchDirection direction = SwitchDirection.up;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Actions.OnSwitchPulled += ChangeSwitchState;
        Actions.OnSwitchPulled += ChangeSwitchDirection;
    }

    private void OnDisable()
    {
        Actions.OnSwitchPulled -= ChangeSwitchState;
        Actions.OnSwitchPulled += ChangeSwitchDirection;
    }

    private void ChangeSwitchState()
    {
        if (isInteractable)
        {
            if (GameManager.Instance.currentState == SwitchStates.red)
            {
                GameManager.Instance.currentState = SwitchStates.blue;
                animator.SetTrigger("Switch Touched");
            }
            else
            {
                GameManager.Instance.currentState = SwitchStates.red;
                animator.SetTrigger("Switch Touched");
            } 
        }
    }

    private void ChangeSwitchDirection()
    {
        if (gameObject.CompareTag("Red Switch"))
        {
            GameObject[] blueSwitches = GameObject.FindGameObjectsWithTag("Blue Switch");
            if (direction == SwitchDirection.up)
            {
                direction = SwitchDirection.down;
                animator.SetBool("isDown", true);

                foreach (GameObject o in blueSwitches)
                {
                    o.GetComponent<Animator>().SetBool("isDown", false);
                    o.GetComponent<Animator>().SetTrigger("Switch Touched");
                }
            }
            else
            {
                direction = SwitchDirection.up;
                animator.SetBool("isDown", false);
                foreach (GameObject o in blueSwitches)
                {
                    o.GetComponent<Animator>().SetBool("isDown", true);
                    o.GetComponent<Animator>().SetTrigger("Switch Touched");
                }
            }
        }
        else if (gameObject.CompareTag("Blue Switch"))
        {
            GameObject[] redSwitches = GameObject.FindGameObjectsWithTag("Red Switch");
            if (direction == SwitchDirection.up)
            {
                direction = SwitchDirection.down;
                animator.SetBool("isDown", false);
                foreach (GameObject o in redSwitches)
                {
                    o.GetComponent<Animator>().SetBool("isDown", true);
                    o.GetComponent<Animator>().SetTrigger("Switch Touched");
                }
            }
            else
            {
                direction = SwitchDirection.up;
                animator.SetBool("isDown", true);
                foreach (GameObject o in redSwitches)
                {
                    o.GetComponent<Animator>().SetBool("isDown", false);
                    o.GetComponent<Animator>().SetTrigger("Switch Touched");
                }
            }
        }
        StartCoroutine(WaitForAnimation());
        
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        Actions.OnSwitchDoneMoving();
    }
}
