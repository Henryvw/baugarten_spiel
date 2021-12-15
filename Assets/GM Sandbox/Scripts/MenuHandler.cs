using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject buildingsPanel = default;
    [SerializeField] private GameObject cropsPanel = default;
    [SerializeField] private GameObject formulasPanel = default;

    private List<GameObject> allPanels = new List<GameObject>();

    private void Start()
    {
        allPanels.Add(buildingsPanel);
        allPanels.Add(cropsPanel);
        allPanels.Add(formulasPanel);
        ClosePanels();
    }

    public void OpenBuildingsPanel()
    {
        SwitchTo(buildingsPanel);
    }

    public void OpenCropsPanel()
    {
        SwitchTo(cropsPanel);
    }

    public void OpenFormulasPanel()
    {
        SwitchTo(formulasPanel);
    }

    public void ClosePanels()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }
    }

    private void SwitchTo(GameObject targetPanel)
    {
        if (targetPanel.activeSelf)
        {
            ClosePanels();
        }
        else
        {
            foreach (GameObject panel in allPanels)
            {
                panel.SetActive(panel == targetPanel);
            }
        }
    }
}
