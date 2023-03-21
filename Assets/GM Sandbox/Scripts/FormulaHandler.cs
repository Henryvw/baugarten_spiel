using UnityEngine;
using UnityEngine.UI;

public class FormulaHandler : MonoBehaviour
{
	[Header("General Parameters")]
	[SerializeField] private GameObject formulaPanel = default;
	[SerializeField] private TextDisplay totalAreaText = default;

	[Header("Equilateral Parameters")]
	[SerializeField] private GameObject equilateralPanel = default;
	[SerializeField] private TextDisplay sideText = default;

	[Header("Isosceles Parameters")]
	[SerializeField] private GameObject isoscelesPanel = default;
	[SerializeField] private TextDisplay baseText = default;
	[SerializeField] private TextDisplay heightText = default;

	[Header("Rectangle Parameters")]
	[SerializeField] private GameObject rectanglePanel = default;
	[SerializeField] private TextDisplay widthText = default;
	[SerializeField] private TextDisplay lengthText = default;

	private Field currentField;
	private AreaType currentAreaType;
	private int totalArea;
	private int sideLength;
	private int baseLength;

	private void Start()
	{
		formulaPanel.SetActive(false);
	}

	private void UpdateTextDisplay()
	{
		sideLength = currentField.GetSelectedPreset().GetSideLengthHeight();
		baseLength = currentField.GetSelectedPreset().GetBaseWidth();

		sideText.SetTextToFloat(sideLength);
		heightText.SetTextToFloat(sideLength);
		baseText.SetTextToFloat(baseLength);
		widthText.SetTextToFloat(baseLength);
		lengthText.SetTextToFloat(sideLength);
	}

	public void OpenPanel(Field field)
	{
		currentField = field;
		formulaPanel.SetActive(true);
		UpdateTextDisplay();

		switch (currentAreaType)
		{
			case AreaType.Rectangle:
				totalArea = AreaFormulas.GetRectangleArea(sideLength, baseLength);
				break;
			case AreaType.Equilateral:
				totalArea = AreaFormulas.GetEquilateralArea(sideLength);
				break;
			case AreaType.Isosceles:
				totalArea = AreaFormulas.GetIsoscelesArea(sideLength, baseLength);
				break;
		}
		totalAreaText.SetTextToFloat(totalArea);
	}

	public void ClosePanel()
	{
		currentField = null;
		formulaPanel.SetActive(false);
	}

	public void ToggleEquilateralPanel()
	{
		equilateralPanel.SetActive(true);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(false);
		currentAreaType = AreaType.Equilateral;
	}

	public void ToggleIsoscelesPanel()
	{
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(true);
		rectanglePanel.SetActive(false);
		currentAreaType = AreaType.Isosceles;
	}

	public void ToggleRectanglePanel()
	{
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(true);
		currentAreaType = AreaType.Rectangle;
	}
}
