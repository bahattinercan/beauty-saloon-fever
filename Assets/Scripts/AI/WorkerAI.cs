using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private StackManager stackManager;
    public EStack eStack;
    public WorkPoint workPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stackManager = GetComponent<StackManager>();
        agent.speed = GameManager.instance.helperSpeed;
        stackManager.maxStackNumber = (int)GameManager.instance.helperStackCapacity;
        WorkerController.instance.SendToStackSpawners(this);
        GameManager.instance.OnHelperSpeed += GameManager_onHelperSpeed;
        GameManager.instance.OnHelperStackCapacity += GameManager_onHelperStackCapacity;
    }

    private void GameManager_onHelperStackCapacity(float maxCapacity)
    {
        stackManager.maxStackNumber = (int)maxCapacity;
    }

    private void GameManager_onHelperSpeed(float speed)
    {
        agent.speed = speed;        
    }

    public void Move(Vector3 pos)
    {
        agent.destination = pos;
    }

    public bool IsWorkingHere(WorkPoint workPoint)
    {
        if (this.workPoint = workPoint)
            return true;
        return false;
    }
}