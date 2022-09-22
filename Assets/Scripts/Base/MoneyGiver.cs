using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGiver : MonoBehaviour
{
    MoneyMaker moneyMaker;
    [SerializeField] private float giveMoneyDelay = .01f;
    Collider other;

    private void Start()
    {
        moneyMaker = transform.parent.GetComponent<MoneyMaker>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.other = other;
            InvokeRepeating("GiveMoney", 0, giveMoneyDelay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke();
        }
    }

    private void GiveMoney()
    {
        moneyMaker.GiveMoney();
        GameManager.instance.UpdateMoney();
    }
}
