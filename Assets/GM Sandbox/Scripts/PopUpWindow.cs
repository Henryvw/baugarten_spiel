using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private Text mainText = default;

    public void SetMainTextTo(string newText)
    {
        mainText.text = newText;
    }

    public void OnClose()
    {
        Destroy(this.gameObject);
    }
}
