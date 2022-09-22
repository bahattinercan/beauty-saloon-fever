using UnityEngine;

public class HelperManager : MonoBehaviour
{
    public Upgrade hireUpgrade, speedUpgrade, capacityUpgrade;

    public void HireUpgrade()
    {
        GameManager.instance.DecreaseMoney(hireUpgrade.neededMoney);
        GameManager.instance.HireWorker(GameObject.Find("workerSpawnPoint").transform.position);
        hireUpgrade.Update();
    }

    public void SpeedUpgrade()
    {
        GameManager.instance.DecreaseMoney(speedUpgrade.neededMoney);
        GameManager.instance.SpeedUp_HelperMovement(speedUpgrade.upgradeValue);
        speedUpgrade.Update();
    }

    public void CapacityUpgrade()
    {
        GameManager.instance.DecreaseMoney(capacityUpgrade.neededMoney);
        GameManager.instance.Add_HelperCapacity(capacityUpgrade.upgradeValue);
        capacityUpgrade.Update();
    }    
}