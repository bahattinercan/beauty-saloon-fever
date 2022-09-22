using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI moneyText;
    public Transform canvasT;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        canvasT = GameObject.Find("Canvas").transform;
        moneyText = canvasT.Find("moneyText").GetComponent<TextMeshProUGUI>();
        SetMoneyText(GameManager.instance.money);
    }

    public void SetMoneyText(int moneyValue)
    {
        moneyText.text = FormatMoney(moneyValue);
    }

    public string FormatMoney(int money)
    {

        string text = "$";
        if (money >= 100000)
        {
            text += money / 1000;
            text += "k";
        }
        else if (money >= 10000)
        {
            text += ((float)money / 1000).ToString("F1");
            text += "k";
        }
        else if (money >= 1000)
        {
            text +=  ((float)money / 1000).ToString("F2");
            text += "k";
        }
        else if (money < 1000)
        {
            text += money;
        }
        return text;
    }
}