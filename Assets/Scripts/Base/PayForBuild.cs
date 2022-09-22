using TMPro;
using UnityEngine;

public class PayForBuild : MonoBehaviour
{
    private int currentMoney;
    [SerializeField] private int neededMoney;
    private TextMeshPro text;
    private Transform progressBar;
    private Vector3 fullScale;
    public int getMoneyNumber = 5;
    private float waitForGive = .1f;
    private float giveMoneyToBuildDelay = .001f;
    [SerializeField] private EBuilding eBuilding;

    private void Start()
    {
        text = transform.Find("moneyText").GetComponent<TextMeshPro>();
        progressBar = transform.Find("progress");
        fullScale = progressBar.localScale;
        progressBar.localScale = new Vector3(0, fullScale.y, fullScale.z);
        text.text = UIManager.instance.FormatMoney(neededMoney);        
    }

    private void UpdateMoney()
    {
        if (GameManager.instance.HasMoney(getMoneyNumber))
        {
            if (NeedMoney())
            {
                GameManager.instance.DecreaseMoney(getMoneyNumber);
                currentMoney += getMoneyNumber;
                if (!NeedMoney())
                {
                    BuildThePlace();
                }
                else
                {
                    progressBar.localScale = new Vector3(fullScale.x * ((float)currentMoney / neededMoney), fullScale.y, fullScale.z);
                    text.text = UIManager.instance.FormatMoney(neededMoney - currentMoney);
                }                
            }
        }
    }

    private bool NeedMoney()
    {
        if (currentMoney < neededMoney)
            return true;
        return false;
    }

    private void BuildThePlace()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + GameManager.instance.spawnPlusY, transform.position.z);
        Instantiate(GameManager.instance.GetBuildPrefab(eBuilding), spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("UpdateMoney", waitForGive, giveMoneyToBuildDelay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("UpdateMoney");
        }
    }
}