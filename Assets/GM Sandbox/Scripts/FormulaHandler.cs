using UnityEngine;
using UnityEngine.UI;

public class FormulaHandler : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private int sideLength = 1;

	[Header("Object References")]
	[SerializeField] private GameObject formulaPanel = default;
	[SerializeField] private TextDisplay lengthText = default;
	[SerializeField] private TextDisplay totalAreaText = default;

	private Field currentField;
	private int totalArea;

	private void Start()
	{
		formulaPanel.SetActive(false);
		UpdateFormulaDisplay();
	}

	private void UpdateFormulaDisplay()
	{
		lengthText.SetTextToFloat(sideLength);
		totalArea = (int)Mathf.Round((Mathf.Sqrt(3) / 4) * sideLength * sideLength);
		totalAreaText.SetTextToFloat(totalArea);
	}

	public void OpenPanel(Field field)
	{
		currentField = field;
		formulaPanel.SetActive(true);
		UpdateFormulaDisplay();
	}

	public void ClosePanel()
	{
		currentField = null;
		formulaPanel.SetActive(false);
	}
}