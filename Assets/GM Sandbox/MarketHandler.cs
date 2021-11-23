using UnityEngine;
using UnityEngine.UI;

public class MarketHandler : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private int fixedCost = 100;
	[SerializeField] private int perSeedCost = 1;

	[Header("Object References")]
	[SerializeField] private GameObject marketPanel = default;
	[SerializeField] private Slider seedsSlider = default;
	[SerializeField] private TextDisplay fixedCostText = default;
	[SerializeField] private TextDisplay seedCostText = default;
	[SerializeField] private TextDisplay totalCostText = default;

	private Field currentField;
	private int seedCount;
	private int totalCost;

	private void Start()
	{
		marketPanel.SetActive(false);
		UpdateCostDisplay();
	}

	public void UpdateCostDisplay()
	{
		int seedCount = (int)seedsSlider.value;
		int totalCost = fixedCost + perSeedCost * seedCount;
		fixedCostText.SetTextToFloat(fixedCost);
		seedCostText.SetTextToFloat(perSeedCost * seedCount);
		totalCostText.SetTextToFloat(totalCost);
	}

	public void StartSeedPurchaseForField(Field field)
	{
		currentField = field;
		ToggleMarketPanel();
	}

	public void ToggleMarketPanel()
	{
		marketPanel.SetActive(!marketPanel.activeSelf);
	}

	public void PurchaseSeeds()
	{
		if (currentField == null) { Debug.LogError("No target field set for Market"); }

		if (EconomyManager.Instance.totalMoney >= totalCost)
		{
			Debug.Log(EconomyManager.Instance.totalMoney);
			EconomyManager.Instance.totalMoney -= totalCost;
			currentField.PlantField(seedCount);
			currentField = null;
			ToggleMarketPanel();
		}
		else
		{
			Debug.Log("Not enough money to purchase seeds.");
		}
	}
}