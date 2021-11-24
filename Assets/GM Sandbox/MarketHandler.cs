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
		seedCount = (int)seedsSlider.value;
		totalCost = fixedCost + perSeedCost * seedCount;
		fixedCostText.SetTextToFloat(fixedCost);
		seedCostText.SetTextToFloat(perSeedCost * seedCount);
		totalCostText.SetTextToFloat(totalCost);
	}

	public void OpenPanel(Field field)
	{
		currentField = field;
		marketPanel.SetActive(true);
	}

	public void ClosePanel()
	{
		currentField = null;
		marketPanel.SetActive(false);
	}

	public void PurchaseSeeds()
	{
		if (currentField == null) { Debug.LogError("No target field set for Market"); return; }

		if (EconomyManager.Instance.totalMoney >= totalCost)
		{
			EconomyManager.Instance.totalMoney -= totalCost;
			currentField.PlantField(seedCount);
			ClosePanel();
		}
		else
		{
			Debug.Log("Not enough money to purchase seeds.");
		}
	}
}