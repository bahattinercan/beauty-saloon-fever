using System.Collections.Generic;
using UnityEngine;

public class StackGetter : MonoBehaviour
{
    private StackManager stackManager;
    [SerializeField] private float giveStackDelay = .05f;
    private Collider player;
    private List<Collider> colliders;

    private void Start()
    {
        colliders = new List<Collider>();
        stackManager = transform.GetComponentInParent<StackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<StackManager>().stackType == stackManager.stackType)
            {
                player = other;
                InvokeRepeating("GetStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, giveStackDelay);
            }
        }
        else if (other.CompareTag("Worker"))
        {
            WorkerAI worker = other.GetComponent<WorkerAI>();
            if (other.GetComponent<StackManager>().stackType == stackManager.stackType &&
                worker.IsWorkingHere(stackManager.GetComponent<WorkPoint>()))
            {
                colliders.Add(other);
                InvokeRepeating("GetStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, giveStackDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<StackManager>().stackType == stackManager.stackType)
            {
                CancelInvoke("GetStack" + other.tag);
            }
        }
        else if (other.CompareTag("Worker"))
        {
            if (other.GetComponent<StackManager>().stackType == stackManager.stackType &&
                other.GetComponent<WorkerAI>().IsWorkingHere(this.stackManager.GetComponent<WorkPoint>()))
            {
                CancelInvoke("GetStack" + other.tag);
                colliders.Remove(other);
                if (colliders.Count > 0)
                {
                    InvokeRepeating("GetStack" + other.tag, GameManager.instance.StartDelay_GiveAndGetStack, GameManager.instance.playerCollectDelay);
                }
            }
        }
    }


    private void GetStackPlayer()
    {
        StackManager otherStack = player.transform.GetComponent<StackManager>();
        if (otherStack.stackNumber > 0 && stackManager.NeedStack())
        {
            stackManager.Pickup(otherStack.GetStack());
        }
    }

    private void GetStackWorker()
    {
        foreach (Collider item in colliders)
        {
            StackManager otherStack = item.transform.GetComponent<StackManager>();
            if (otherStack.stackNumber > 0 && stackManager.NeedStack())
            {
                stackManager.Pickup(otherStack.GetStack());
            }
            if (otherStack.HasStack() == true && !stackManager.NeedStack())
            {
                WorkerController.instance.SendToMoneyMakers(item.GetComponent<WorkerAI>());
            }
            else if (otherStack.HasStack() == false)
            {
                WorkerController.instance.SendToStackSpawners(item.GetComponent<WorkerAI>());
            }
        }
    }
}