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
	private int sideLength;

	[Header("Isosceles Parameters")]
	[SerializeField] private GameObject isoscelesPanel = default;
	[SerializeField] private TextDisplay baseText = default;
	[SerializeField] private TextDisplay heightText = default;
	private int baseLength;
	private int heightLength;

	[Header("Rectangle Parameters")]
	[SerializeField] private GameObject rectanglePanel = default;
	[SerializeField] private TextDisplay widthText = default;
	[SerializeField] private TextDisplay lengthText = default;
	private int widthLength;
	private int rectangleLength;

	private Field currentField;
	private int totalArea;

	private void Start()
	{
		formulaPanel.SetActive(false);
		UpdateTextDisplay();
	}

	private void UpdateTextDisplay()
	{
		SetFieldParameters();
		sideText.SetTextToFloat(sideLength);
		heightText.SetTextToFloat(heightLength);
		baseText.SetTextToFloat(baseLength);
		widthText.SetTextToFloat(widthLength);
		lengthText.SetTextToFloat(rectangleLength);
	}

	private void SetFieldParameters()
	{
		sideLength = currentField.GetSelectedPreset().sideLength;
		heightLength = currentField.GetSelectedPreset().heightLength;
		baseLength = currentField.GetSelectedPreset().baseLength;
		widthLength = currentField.GetSelectedPreset().widthLength;
		rectangleLength = currentField.GetSelectedPreset().rectangleLength;
	}

	private int GetEquilateralArea()
	{
		int area = (int)Mathf.Round((Mathf.Sqrt(3) / 4) * sideLength * sideLength);
		return area;
	}

	private int GetIsoscelesArea()
	{
		int area = (int)Mathf.Round((heightLength * baseLength) / 2);
		return area;
	}

	private int GetRectangleArea()
	{
		int area = (int)Mathf.Round(widthLength * rectangleLength);
		return area;
	}

	public void OpenPanel(Field field)
	{
		currentField = field;
		formulaPanel.SetActive(true);
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
		totalAreaText.transform.parent.gameObject.SetActive(true);

		UpdateTextDisplay();
		totalArea = GetEquilateralArea();
		totalAreaText.SetTextToFloat(totalArea);
	}

	public void ToggleIsoscelesPanel()
	{
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(true);
		rectanglePanel.SetActive(false);
		totalAreaText.transform.parent.gameObject.SetActive(true);

		UpdateTextDisplay();
		totalArea = GetIsoscelesArea();
		totalAreaText.SetTextToFloat(totalArea);
	}

	public void ToggleRectanglePanel()
	{
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(true);
		totalAreaText.transform.parent.gameObject.SetActive(true);

		UpdateTextDisplay();
		totalArea = GetRectangleArea();
		totalAreaText.SetTextToFloat(totalArea);
	}
}