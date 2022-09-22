using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPoint : MonoBehaviour
{
    public bool needWorker;
    private Vector3 workPos;
    public EStack eStack;
    public bool isSpawner;
    private StackSpawner stackSpawner;
    private StackManager stackManager;

    public Vector3 WorkPos { get => workPos; }

    private void Start()
    {
        if (transform.Find("stackGiver") != null)
        {
            workPos = transform.Find("stackGiver").position;
            stackSpawner = GetComponent<StackSpawner>();
            eStack =stackSpawner.stackType;
            
        }            
        else
        {
            workPos = transform.Find("stackGetter").position;
            stackManager = GetComponent<StackManager>();
            eStack = stackManager.stackType;
        }
        WorkerController.instance.Add_WorkPointList(this);
        InvokeRepeating("HasNeedWork", 0, .1f);
    }
    // need workü invoke repeat ile düzenle

    private void HasNeedWork()
    {
        if (isSpawner)
            needWorker = stackSpawner.NeedWork();
        else
            needWorker = stackManager.NeedWork();
    }
}
