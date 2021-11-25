using UnityEngine;
using UnityEngine.UI;

public class FormulaHandler : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private int sideLength = 1;

	[Header("Object References")]
	[SerializeField] private GameObject formulaPanel = default;
	[SerializeField] private TextDisplay fixedFormulaText = default;
	[SerializeField] private TextDisplay variableFormulaText = default;
	[SerializeField] private TextDisplay totalAreaText = default;

	private Field currentField;
	private int totalArea;

	private void Start()
	{
		formulaPanel.SetActive(false);
		UpdateFormulaDisplay();
	}

	public void UpdateFormulaDisplay()
	{
		totalArea = (int)Mathf.Round((Mathf.Sqrt(3) / 4) * sideLength * sideLength);
		totalAreaText.SetTextToFloat(totalArea);
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
}