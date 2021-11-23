using UnityEngine;
using UnityEngine.UI;

public class SeedsCountDisplay : MonoBehaviour
{
	public void SetSeedCountText(float sliderValue)
	{
		GetComponent<Text>().text = Mathf.Round(sliderValue).ToString();
	}
}
