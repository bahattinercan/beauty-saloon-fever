using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnimController : MonoBehaviour
{
    public Animator anim;
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    private void Update()
    {
        if (agent.velocity.sqrMagnitude > 1.5f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);            
        }
    }
}
