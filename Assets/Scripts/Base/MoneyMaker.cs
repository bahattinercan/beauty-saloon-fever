using UnityEngine;

public class MoneyMaker : MonoBehaviour
{
    private float distanceY, distanceZ;
    private Vector3 prevPos;
    private Vector3 firstPos;
    public Transform parent;
    private StackManager stackManager;
    private int stackNumber;
    private int money = 1, maxMoney = 80, moneyCost = 2;
    private GameObject prefab;

    private void Start()
    {
        prefab = GameManager.instance.moneyP;
        parent = transform.Find("moneyParent");
        Transform prevObject = parent.GetChild(0);
        prevPos= prevObject.position;
        firstPos = prevObject.position;
        distanceZ = prevObject.localScale.z + .16f;
        distanceY = prevObject.localScale.y;
        InvokeRepeating("GetStack", 0f, (GameManager.instance.workerSpawnDelay / moneyCost) - .001f);
        InvokeRepeating("SpawnMoney", 0f, GameManager.instance.workerSpawnDelay);
        stackManager = GetComponent<StackManager>();
        GameManager.instance.onWorkerSpawnDelay += GameManager_OnWorkerSpawnDelay;
    }

    private void GameManager_OnWorkerSpawnDelay()
    {
        CancelInvoke();
        InvokeRepeating("GetStack", 0f, (GameManager.instance.workerSpawnDelay / moneyCost) - .001f);
        InvokeRepeating("SpawnMoney", 0f, GameManager.instance.workerSpawnDelay);
    }

    private void GetStack()
    {
        if (stackManager.HasStack() & money < maxMoney)
        {
            stackNumber++;
            Destroy(stackManager.GetStack());
        }
    }

    private void SpawnMoney()
    {
        if (money < maxMoney && stackNumber >= moneyCost)
        {
            stackNumber -= moneyCost;
            Vector3 newLoc = prevPos;
            if (money % 4 != 0)
            {
                newLoc.z += distanceZ;
            }
            GameObject stackGo = Instantiate(prefab, newLoc, Quaternion.identity, parent);
            stackGo.transform.Rotate(Vector3.up * Random.Range(-GameManager.instance.StackRotation, GameManager.instance.StackRotation));
            money++;
            if (money % 4 == 0 && money < 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + ((money / 4) * distanceY),
                    firstPos.z);
            }
            else if (money % 4 == 0 && money > 40)
            {
                prevPos = new Vector3(firstPos.x - distanceZ,
                    firstPos.y + (((money - 40) / 4) * distanceY),
                    firstPos.z);
            }
            else
            {
                prevPos = stackGo.transform.position;
            }
            if (money == 40)
                prevPos = new Vector3(firstPos.x - distanceZ, firstPos.y, firstPos.z);
            
        }
    }

    public void GiveMoney()
    {
        if (money > 1)
        {
            prevPos = parent.GetChild(money - 2).position;
            Destroy(parent.GetChild(money - 1).gameObject);
            GameManager.instance.AddMoney();
            --money;

            if (money % 4 == 0 && money < 40)
            {
                prevPos = new Vector3(firstPos.x,
                    firstPos.y + ((money / 4) * distanceY),
                    firstPos.z);
            }
            else if (money % 4 == 0 && money > 40)
            {
                prevPos = new Vector3(firstPos.x - distanceZ,
                    firstPos.y + (((money - 40) / 4) * distanceY),
                    firstPos.z);
            }
            if (money == 40)
                prevPos = new Vector3(firstPos.x - distanceZ, firstPos.y, firstPos.z);
        }
        else if (money == 1)
        {
            prevPos = firstPos;
            Destroy(parent.GetChild(money - 1).gameObject);
            GameManager.instance.AddMoney();
            --money;
        }
    }
}