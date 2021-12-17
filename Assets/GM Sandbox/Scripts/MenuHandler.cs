using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        foreach (GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }
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
            if (panel.activeSelf)
            {
                StartCoroutine(DisablePanel(panel));
            }
        }
    }

    private void SwitchTo(GameObject targetPanel)
    {
        if (targetPanel.activeSelf)
        {
            StartCoroutine(DisablePanel(targetPanel));
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            foreach (GameObject panel in allPanels)
            {
                panel.SetActive(panel == targetPanel);
            }
        }
    }

    private IEnumerator DisablePanel(GameObject panel)
    {
        panel.GetComponent<Animator>().SetTrigger("SlideOut");
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }
}
