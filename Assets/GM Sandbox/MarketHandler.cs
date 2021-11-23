using UnityEngine;
using UnityEngine.UI;

public class MarketHandler : MonoBehaviour
{
	[SerializeField] private GameObject marketPanel = default;
	[SerializeField] private Slider seedsSlider = default;

	private Field currentField;

	private void Start()
	{
		marketPanel.SetActive(false);
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

		int seedCount = (int)seedsSlider.value;

		currentField.PlantField(seedCount);
		currentField = null;
		ToggleMarketPanel();
	}
}