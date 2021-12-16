using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDeselectHandler : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private bool isActive = true;
    [SerializeField] private MenuHandler menuHandler = default;

    public void OnDeselect(BaseEventData data)
    {
        if (!isActive) { return; }

        menuHandler.ClosePanels();
    }
}
