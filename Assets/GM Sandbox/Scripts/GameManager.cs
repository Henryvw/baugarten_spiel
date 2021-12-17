using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int winTargetCurrency = 1000;
    [SerializeField] private int loseTargetCurrency = 0;
    [SerializeField] private float resetGameDelay = 1f;

    public void TryWinOrLose()
    {
        if (EconomyManager.Instance.totalMoney <= loseTargetCurrency)
        {
            TriggerGameOver();
        }
        else if (EconomyManager.Instance.totalMoney >= winTargetCurrency)
        {
            TriggerWin();
        }
    }

    private void TriggerGameOver()
    {
        FindObjectOfType<PopUpHandler>().CreateNewPopUp("Game Over");
        StartCoroutine(ResetGame());
    }

    private void TriggerWin()
    {
        FindObjectOfType<PopUpHandler>().CreateNewPopUp("Game Won!");
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(resetGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
