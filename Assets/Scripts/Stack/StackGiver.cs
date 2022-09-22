using System.Collections.Generic;
using UnityEngine;

public class StackGiver : MonoBehaviour
{
    private StackSpawner stackSpawner;
    private Collider player;
    private List<Collider> colliders;

    private void Start()
    {
        colliders = new List<Collider>();
        stackSpawner = transform.parent.GetComponent<StackSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StackManager stackManager = other.GetComponent<StackManager>();
            if (stackManager.stackType == stackSpawner.stackType || stackManager.stackNumber == 0)
            {
                stackManager.stackType = stackSpawner.stackType;
                player = other;
                InvokeRepeating("GiveStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, GameManager.instance.playerCollectDelay);
            }
        }
        if (other.CompareTag("Worker"))
        {
            StackManager stackManager = other.GetComponent<StackManager>();
            if (stackManager.stackType == stackSpawner.stackType || stackManager.stackNumber == 0 &&
                other.GetComponent<WorkerAI>().IsWorkingHere(this.stackSpawner.GetComponent<WorkPoint>()))
            {
                stackManager.stackType = stackSpawner.stackType;
                colliders.Add(other);
                InvokeRepeating("GiveStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, GameManager.instance.playerCollectDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<StackManager>().stackType == stackSpawner.stackType)
            {
                CancelInvoke("GiveStack" + other.tag);
            }
        }
        else if (other.CompareTag("Worker"))
        {
            if (other.GetComponent<StackManager>().stackType == stackSpawner.stackType &&
                other.GetComponent<WorkerAI>().IsWorkingHere(this.stackSpawner.GetComponent<WorkPoint>()))
            {
                CancelInvoke("GiveStack" + other.tag);
                colliders.Remove(other);
                if (colliders.Count > 0)
                {
                    InvokeRepeating("GiveStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, GameManager.instance.playerCollectDelay);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Worker") && other.GetComponent<StackManager>().stackType == stackSpawner.stackType)
        {
        }
    }

    private void GiveStackPlayer()
    {
        stackSpawner.GiveStack(player.transform.GetComponent<StackManager>());
    }

    private void GiveStackWorker()
    {
        foreach (Collider item in colliders)
        {
            stackSpawner.GiveStack(item.transform.GetComponent<StackManager>());
        }
    }
}