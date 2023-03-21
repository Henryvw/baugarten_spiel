using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameWonText = "Game Won!";
    [SerializeField] private string gameLostText = "Game Over";
    [SerializeField] private int winTargetCurrency = 3000;
    [SerializeField] private int loseTargetCurrency = 0;
    [SerializeField] private float resetGameDelay = 1f;

    private bool gameHasEnded = false;

    public void TryWinOrLose()
    {
        if (gameHasEnded) { return; }

        if (EconomyManager.Instance.totalMoney <= loseTargetCurrency)
        {
            gameHasEnded = true;
            StartCoroutine(TriggerGameOver());
        }
        else if (EconomyManager.Instance.totalMoney >= winTargetCurrency)
        {
            gameHasEnded = true;
            StartCoroutine(TriggerWin());
        }
    }

    private IEnumerator TriggerGameOver()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PopUpHandler>().CreateNewPopUp(gameLostText);
        StartCoroutine(ResetGame());
    }

    private IEnumerator TriggerWin()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PopUpHandler>().CreateNewPopUp(gameWonText);
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(resetGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
