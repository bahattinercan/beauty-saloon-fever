using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Upgrade playerCapacityUpgrade, collectSpeedUpgrade, workerSpeedUpgrade;

    public void UpgradePlayerCapacity()
    {
        GameManager.instance.DecreaseMoney(playerCapacityUpgrade.neededMoney);
        GameManager.instance.Add_PlayerStackCapacity((int)playerCapacityUpgrade.upgradeValue);
        playerCapacityUpgrade.Update();
    }

    public void UpgradeCollectSpeed()
    {
        GameManager.instance.DecreaseMoney(collectSpeedUpgrade.neededMoney);
        GameManager.instance.SpeedUp_PlayerCollect(collectSpeedUpgrade.upgradeValue);
        collectSpeedUpgrade.Update();
    }

    public void UpgradeWorkerSpeed()
    {
        GameManager.instance.DecreaseMoney(workerSpeedUpgrade.neededMoney);
        GameManager.instance.SpeedUp_WorkerWork(workerSpeedUpgrade.upgradeValue);
        workerSpeedUpgrade.Update();
    }
}
