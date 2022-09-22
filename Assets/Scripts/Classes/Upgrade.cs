[System.Serializable]
public class Upgrade
{
    public int neededMoney;
    public float upgradeValue;
    public int upgradeIncreaseNeededMoney;
    public int upgradeNumber;
    public int maxUpgradeNumber;

    public void Update()
    {
        neededMoney += upgradeIncreaseNeededMoney;
        upgradeNumber++;
    }

    public bool CanUpgrade()
    {
        return upgradeNumber < maxUpgradeNumber;
    }
}
