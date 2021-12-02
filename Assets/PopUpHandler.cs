using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    [SerializeField] private PopUpWindow popUpPrefab = default;

    public void CreateNewPopUp(string popUpText)
    {
        PopUpWindow newPopUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);

        if (popUpText != null)
        {
            newPopUp.SetMainTextTo(popUpText);
        }
    }
}
