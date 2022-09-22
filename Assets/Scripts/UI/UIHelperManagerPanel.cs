using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHelperManagerPanel : MonoBehaviour
{
    private HelperManager helperManager;
    private Button hirePanelButton;
    private Button speedPanelButton;
    private Button capacityPanelButton;

    private void Start()
    {
        helperManager = GetComponent<HelperManager>();
        transform.Find("disablePanelButton").GetComponent<Button>().onClick.AddListener(DisablePanel);

        hirePanelButton = transform.Find("hirePanel/button").GetComponent<Button>();
        speedPanelButton = transform.Find("speedPanel/button").GetComponent<Button>();
        capacityPanelButton = transform.Find("capacityPanel/button").GetComponent<Button>();

        hirePanelButton.onClick.AddListener(helperManager.HireUpgrade);
        hirePanelButton.onClick.AddListener(UpdateHireButton);
        hirePanelButton.onClick.AddListener(DisablePanel);
        speedPanelButton.onClick.AddListener(helperManager.SpeedUpgrade);
        speedPanelButton.onClick.AddListener(UpdateSpeedButton);
        speedPanelButton.onClick.AddListener(DisablePanel);
        capacityPanelButton.onClick.AddListener(helperManager.CapacityUpgrade);
        capacityPanelButton.onClick.AddListener(UpdateCapacityButton);
        capacityPanelButton.onClick.AddListener(DisablePanel);

        UpdateButtonInteractables();
        DisablePanel();
    }

    private void OnEnable()
    {
        if (hirePanelButton != null)
            UpdateButtonInteractables();
    }

    public void UpdateButtonText(Button button, int value)
    {
        button.transform.Find("text").GetComponent<TextMeshProUGUI>().text =
            UIManager.instance.FormatMoney(value);
    }

    public void SetButtonInteractable(Button button, Upgrade upgrade)
    {
        if (GameManager.instance.HasMoney(upgrade.neededMoney) && upgrade.CanUpgrade())
            button.interactable = true;
        else
            button.interactable = false;
    }

    private void UpdateButtonInteractables()
    {
        UpdateHireButton();
        UpdateSpeedButton();
        UpdateCapacityButton();
    }
    private void UpdateHireButton()
    {
        UpdateButtonText(hirePanelButton, helperManager.hireUpgrade.neededMoney);
        SetButtonInteractable(hirePanelButton, helperManager.hireUpgrade);
    }

    private void UpdateSpeedButton()
    {
        UpdateButtonText(speedPanelButton, helperManager.speedUpgrade.neededMoney);
        SetButtonInteractable(speedPanelButton, helperManager.speedUpgrade);
    }

    private void UpdateCapacityButton()
    {
        UpdateButtonText(capacityPanelButton, helperManager.capacityUpgrade.neededMoney);
        SetButtonInteractable(capacityPanelButton, helperManager.capacityUpgrade);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}