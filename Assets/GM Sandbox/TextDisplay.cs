using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
	public void SetTextToFloat(float value)
	{
		GetComponent<Text>().text = Mathf.Round(value).ToString();
	}
}
