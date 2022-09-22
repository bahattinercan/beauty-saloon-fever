using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        GetComponent<JoystickPlayerExample>().moveDirectionIsChanged += moveDirectionIsChanged;
        GetComponent<StackManager>().stackNumberChanged += StackManager_StackNumberChanged;
        anim.SetBool("isCarrying", false);
    }

    private void StackManager_StackNumberChanged(int stackNumber)
    {
        if (stackNumber > 0)
        {
            anim.SetBool("isCarrying", true);
        }
        else
        {
            anim.SetBool("isCarrying", false);
        }
    }

    private void moveDirectionIsChanged(Vector3 dir)
    {
        if (dir.sqrMagnitude > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
