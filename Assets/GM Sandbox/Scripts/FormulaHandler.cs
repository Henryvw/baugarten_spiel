using UnityEngine;
using UnityEngine.UI;

public class FormulaHandler : MonoBehaviour
{
	[Header("General Parameters")]
	[SerializeField] private GameObject formulaPanel = default;
	[SerializeField] private GameObject selectionPanel = default;
	[SerializeField] private GameObject backButton = default;
	[SerializeField] private TextDisplay totalAreaText = default;

	[Header("Equilateral Parameters")]
	[SerializeField] private GameObject equilateralPanel = default;
	[SerializeField] private int sideLength = 1;
	[SerializeField] private TextDisplay sideText = default;

	[Header("Isosceles Parameters")]
	[SerializeField] private GameObject isoscelesPanel = default;
	[SerializeField] private int baseLength = 1;
	[SerializeField] private TextDisplay baseText = default;
	[SerializeField] private int heightLength = 1;
	[SerializeField] private TextDisplay heightText = default;

	[Header("Rectangle Parameters")]
	[SerializeField] private GameObject rectanglePanel = default;
	[SerializeField] private int widthLength = 1;
	[SerializeField] private TextDisplay widthText = default;
	[SerializeField] private int rectangleLength = 1;
	[SerializeField] private TextDisplay lengthText = default;

	private Field currentField;
	private int totalArea;

	private void Start()
	{
		formulaPanel.SetActive(false);
		UpdateFormulaDisplay();
	}

	private void UpdateFormulaDisplay()
	{
		sideText.SetTextToFloat(sideLength);
		totalArea = (int)Mathf.Round((Mathf.Sqrt(3) / 4) * sideLength * sideLength);
		totalAreaText.SetTextToFloat(totalArea);
	}

	public void OpenPanel(Field field)
	{
		currentField = field;
		formulaPanel.SetActive(true);
		ToggleSelectionPanel();
	}

	public void ClosePanel()
	{
		currentField = null;
		formulaPanel.SetActive(false);
	}

	public void ToggleEquilateralPanel()
	{
		selectionPanel.SetActive(false);
		equilateralPanel.SetActive(true);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(false);
		backButton.SetActive(true);
		totalAreaText.transform.parent.gameObject.SetActive(true);
		UpdateFormulaDisplay();
	}

	public void ToggleIsoscelesPanel()
	{
		selectionPanel.SetActive(false);
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(true);
		rectanglePanel.SetActive(false);
		backButton.SetActive(true);
		totalAreaText.transform.parent.gameObject.SetActive(true);
		UpdateFormulaDisplay();
	}

	public void ToggleRectanglePanel()
	{
		selectionPanel.SetActive(false);
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(true);
		backButton.SetActive(true);
		totalAreaText.transform.parent.gameObject.SetActive(true);
		UpdateFormulaDisplay();
	}

	public void ToggleSelectionPanel()
	{
		selectionPanel.SetActive(true);
		equilateralPanel.SetActive(false);
		isoscelesPanel.SetActive(false);
		rectanglePanel.SetActive(false);
		backButton.SetActive(false);
		totalAreaText.transform.parent.gameObject.SetActive(false);
	}
}