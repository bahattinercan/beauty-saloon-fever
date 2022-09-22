using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    private float distanceY, distanceX;
    private Vector3 prevPos;
    private Vector3 firstPos;
    public Transform parent;
    private int stack = 1, maxStack = 80;
    private GameObject prefab;
    public EStack stackType;

    private void Start()
    {
        prefab = GameManager.instance.GetPrefab(stackType);
        parent = transform.Find("parent");
        Transform prevObject = parent.GetChild(0);
        this.prevPos = prevObject.position;
        firstPos = prevObject.position;
        distanceX = prevObject.localScale.z + .16f;
        distanceY = prevObject.localScale.y;

        InvokeRepeating("SpawnStack", 0, GameManager.instance.machineSpawnDelay);
        GameManager.instance.onMachineSpawnDelay += GameManager_OnMachineSpawnDelay;
    }

    private void GameManager_OnMachineSpawnDelay()
    {
        CancelInvoke();
        InvokeRepeating("SpawnStack", 0, GameManager.instance.machineSpawnDelay);
    }

    private void SpawnStack()
    {
        if (stack < maxStack)
        {
            Vector3 newLoc = prevPos;
            if (stack % 4 != 0)
            {
                newLoc.x += distanceX;
            }
            GameObject stackGo = Instantiate(prefab, newLoc, Quaternion.identity, parent);
            stackGo.transform.Rotate(Vector3.up * Random.Range(-GameManager.instance.StackRotation, GameManager.instance.StackRotation));
            stack++;
            if (stack % 4 == 0 && stack < 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + (stack / 4 * distanceY),
                    firstPos.z);
            }
            else if (stack % 4 == 0 && stack > 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + ((stack - 40) / 4 * distanceY),
                    firstPos.z - distanceX);
            }
            else
            {
                prevPos = stackGo.transform.position;
            }
            if (stack == 40)
                prevPos = new Vector3(firstPos.x, firstPos.y, firstPos.z - distanceX);
        }
    }

    public void GiveStack(StackManager stackManager)
    {
        if (stack > 1 && stackManager.NeedStack())
        {
            prevPos = parent.GetChild(stack - 2).position;
            stackManager.Pickup(parent.GetChild(stack - 1).gameObject);
            --stack;
            if (stack % 4 == 0 && stack < 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + ((stack / 4) * distanceY),
                    firstPos.z);
            }
            else if (stack % 4 == 0 && stack > 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + (((stack - 40) / 4) * distanceY),
                    firstPos.z - distanceX);
            }
            if (stack == 40)
                prevPos = new Vector3(firstPos.x, firstPos.y, firstPos.z - distanceX);
        }
        else if (stack == 1 && stackManager.NeedStack())
        {
            prevPos = firstPos;
            stackManager.Pickup(parent.GetChild(stack - 1).gameObject);
            --stack;
        }
        if (stackManager.tag == "Worker" && !stackManager.NeedStack())
        {
            WorkerController.instance.SendToMoneyMakers(stackManager.GetComponent<WorkerAI>(), stackType);
        }
    }

    public bool NeedWork()
    {
        if (stack > (maxStack / 4))
        {
            return true;
        }
        return false;
    }
}