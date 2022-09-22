using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPanel : MonoBehaviour
{
    private Manager manager;
    private Button playerCapacityButton;
    private Button collectSpeedButton;
    private Button workerSpeedButton;

    private void Start()
    {
        manager = GetComponent<Manager>();
        transform.Find("disablePanelButton").GetComponent<Button>().onClick.AddListener(DisablePanel);

        playerCapacityButton = transform.Find("playerCapacityPanel/button").GetComponent<Button>();
        collectSpeedButton = transform.Find("collectSpeedPanel/button").GetComponent<Button>();
        workerSpeedButton = transform.Find("workerSpeedPanel/button").GetComponent<Button>();

        playerCapacityButton.onClick.AddListener(manager.UpgradePlayerCapacity);
        playerCapacityButton.onClick.AddListener(UpdatePlayerCapacityButton);
        playerCapacityButton.onClick.AddListener(DisablePanel);
        collectSpeedButton.onClick.AddListener(manager.UpgradeCollectSpeed);
        collectSpeedButton.onClick.AddListener(UpdateCollectSpeedButton);
        collectSpeedButton.onClick.AddListener(DisablePanel);
        workerSpeedButton.onClick.AddListener(manager.UpgradeWorkerSpeed);
        workerSpeedButton.onClick.AddListener(UpdateWorkerSpeedButton);
        workerSpeedButton.onClick.AddListener(DisablePanel);

        UpdateButtonInteractables();
        DisablePanel();
    }

    private void OnEnable()
    {
        if (playerCapacityButton != null)
            UpdateButtonInteractables();
    }

    public void UpdateButtonText(Button button,int value)
    {
        button.transform.Find("text").GetComponent<TextMeshProUGUI>().text =
            UIManager.instance.FormatMoney(value);
    }

    public void SetButtonInteractable(Button button,Upgrade upgrade)
    {
        if (GameManager.instance.HasMoney(upgrade.neededMoney) && upgrade.CanUpgrade())
            button.interactable = true;
        else 
            button.interactable = false;
    }

    private void UpdateButtonInteractables()
    {
        UpdatePlayerCapacityButton();
        UpdateCollectSpeedButton();
        UpdateWorkerSpeedButton();
    }

    private void UpdatePlayerCapacityButton()
    {
        UpdateButtonText(playerCapacityButton,manager.playerCapacityUpgrade.neededMoney);
        SetButtonInteractable(playerCapacityButton, manager.playerCapacityUpgrade);
    }

    private void UpdateCollectSpeedButton()
    {
        UpdateButtonText(collectSpeedButton, manager.collectSpeedUpgrade.neededMoney);
        SetButtonInteractable(collectSpeedButton, manager.collectSpeedUpgrade);
    }

    private void UpdateWorkerSpeedButton()
    {
        UpdateButtonText(workerSpeedButton, manager.workerSpeedUpgrade.neededMoney);
        SetButtonInteractable(workerSpeedButton, manager.workerSpeedUpgrade);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}