using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDeselectHandler : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private MenuHandler menuHandler = default;

    public void OnDeselect(BaseEventData data)
    {
        menuHandler.ClosePanels();
    }
}
