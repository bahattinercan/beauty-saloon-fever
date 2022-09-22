using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform playerT;
    public GameObject scissorP, hairDyeP;
    public GameObject moneyP;
    public GameObject hairDresserP, hairDyerP;
    public GameObject workerP;
    public GameObject hairDyeMakerP, scissorMakerP;
    public int money=0;
    public float spawnPlusY=2.05f;
    public int maxPlayerStackCapacity;
    public float playerCollectDelay;
    private float startDelay_GiveAndGetStack = .01f;
    public float machineSpawnDelay,workerSpawnDelay;
    public float helperSpeed, helperStackCapacity, helperAnimSpeed;
    private float stackRotation = 20;
    public float StackRotation { get => stackRotation;}
    public float StartDelay_GiveAndGetStack { get => startDelay_GiveAndGetStack; }

    public event Action<float> OnHelperSpeed;
    public event Action<float> OnHelperStackCapacity;
    public event Action onMachineSpawnDelay;
    public event Action onWorkerSpawnDelay;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null) instance = this;
    }

    private void Start()
    {
        playerT = GameObject.FindWithTag("Player").transform;
    }

    public GameObject GetPrefab(EStack eStack)
    {
        switch (eStack)
        {
            case EStack.scissors:
                return scissorP;

            case EStack.hairDye:
                return hairDyeP;

            default:
                Debug.Log("we don't have that type prefab -> GameManager.GetPrefab()");
                return null;
        }
    }

    public GameObject GetBuildPrefab(EBuilding building)
    {
        switch (building)
        {
            case EBuilding.hairDresser:
                return hairDresserP;
            case EBuilding.hairDyer:
                return hairDyerP;
            case EBuilding.hairDyeMaker:
                return hairDyeMakerP;
            case EBuilding.scissorMaker:
                return scissorMakerP;
            default:
                return null;
        }
    }

    #region Upgrades

    public void HireWorker(Vector3 spawnPos)
    {
        WorkerAI worker = Instantiate(workerP, spawnPos,Quaternion.identity).GetComponent<WorkerAI>();
        //WorkerController.instance.SendToStackSpawners(worker);
    }

    public void Add_PlayerStackCapacity(int value)
    {
        playerT.GetComponent<StackManager>().maxStackNumber += value;
    }
   
    public void SpeedUp_PlayerCollect(float value)
    {
        playerCollectDelay -= value;
    }

    public void SpeedUp_WorkerWork(float value)
    {
        machineSpawnDelay -= value;
        onMachineSpawnDelay?.Invoke();
        workerSpawnDelay -= value * 4;
        onWorkerSpawnDelay?.Invoke();
    }

    public void SpeedUp_HelperMovement(float value)
    {
        helperSpeed += value;
        OnHelperSpeed.Invoke(helperSpeed);
    }

    public void Add_HelperCapacity(float value)
    {
        helperStackCapacity += value;
        OnHelperStackCapacity.Invoke(helperStackCapacity);
    }
    #endregion

    #region Money Functions

    public void AddMoney(int value = 5)
    {
        money += value;
        UpdateMoney();
    }

    public void DecreaseMoney(int value)
    {
        money -= value;
        UpdateMoney();
    }

    public void SetMoney(int value)
    {
        money = value;
        UpdateMoney();
    }

    public int GetMoney()
    {
        return money;
    }

    public void UpdateMoney()
    {
        UIManager.instance.SetMoneyText(money);
    }

    public bool HasMoney(int value = 1)
    {
        if (money >= value)
            return true;
        return false;
    }

    #endregion Money Functions
}