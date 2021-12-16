using UnityEngine;
using UnityEngine.UI;

public class SubMenuButtonHandler : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool startsLocked = false;
    [SerializeField] private int unlockCost = 100;

    [Header("Object References")]
    [SerializeField] private GameObject lockedButton = default;
    [SerializeField] private GameObject availableButton = default;
    [SerializeField] private TextDisplay costText = default;
    [SerializeField] private Image availableIcon = default;
    [SerializeField] private Image lockedIcon = default;

    private void Start()
    {
        if (startsLocked)
        {
            LockButton();
        }
    }

    private void LockButton()
    {
        lockedIcon.sprite = availableIcon.sprite;
        costText.SetTextToFloat(unlockCost);
        lockedButton.SetActive(true);
        availableButton.SetActive(false);
    }

    private void UnlockButton()
    {
        lockedButton.SetActive(false);
        availableButton.SetActive(true);
    }

    public void TryUnlock()
    {
        if (unlockCost <= EconomyManager.Instance.totalMoney)
        {
            EconomyManager.Instance.totalMoney -= unlockCost;
            UnlockButton();
        }
        else
        {
            Debug.Log("Not enough money to purchase unlock.");
        }
    }

}
