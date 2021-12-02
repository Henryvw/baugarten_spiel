using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private Text mainText = default;
    [SerializeField] private bool freezeTimeOnOpen = true;

    private void Start()
    {
        if (freezeTimeOnOpen)
        {
            Time.timeScale = 0f;
        }
    }

    public void SetMainTextTo(string newText)
    {
        mainText.text = newText;
    }

    public void OnClose()
    {
        if (freezeTimeOnOpen)
        {
            Time.timeScale = 1f;
        }
        
        Destroy(this.gameObject);
    }
}
