using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public static WorkerController instance;
    public List<WorkPoint> moneyMakers;
    public List<WorkPoint> stackSpawners;

    private void Awake()
    {
        if (instance ==null) instance = this;
    }

    public void Add_WorkPointList(WorkPoint workPoint)
    {
        if (workPoint.isSpawner)
        {
            stackSpawners.Add(workPoint);
        }
        else
        {
            moneyMakers.Add(workPoint);
        }
    }

    public void SendToStackSpawners(WorkerAI worker)
    {
        WorkPoint workPoint = stackSpawners[Random.Range(0, stackSpawners.Count)];
        worker.workPoint = workPoint;
        worker.Move(workPoint.WorkPos);        
    }

    public void SendToMoneyMakers(WorkerAI worker,EStack eStack)
    {
        worker.eStack = eStack; // eStack part
        SendToMoneyMakers(worker);
    }

    public void SendToMoneyMakers(WorkerAI worker)
    {

        WorkPoint workPoint = moneyMakers[0];

        for (int i = 0; i < moneyMakers.Count; i++)
        {
            workPoint = moneyMakers[i];
            if (workPoint.eStack == worker.eStack && workPoint.needWorker)
            {
                workPoint = moneyMakers[i];
                break;
            }
        }
        worker.Move(workPoint.WorkPos);
    }
}
