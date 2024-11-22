using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class EndGameUIController: MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private TextMeshProUGUI leftPlayerTimeTouchText;
        [SerializeField] private TextMeshProUGUI rightPlayerTimeTouchText;
        [SerializeField] private Image leftPlayerTextImageWinner;
        [SerializeField] private Image rightPlayerTextImageWinner;
        [SerializeField] private TouchController touchController;

        public void ShowWinner(bool isLeftPlayerWin)
        {
            StartCoroutine(WaitForShowWinnerUI(isLeftPlayerWin));
        }

        private IEnumerator WaitForShowWinnerUI(bool isLeftPlayerWin)
        {
            yield return new WaitForSecondsRealtime(2);
            Debug.Log($"IsLeftPlayerWin: {isLeftPlayerWin} -- Set mainPanel.SetActive(true)");
            mainPanel.SetActive(true);
            var playerOneMilliseconds = (int)(touchController.playerOneDateTime - touchController.startDateTime).TotalMilliseconds;
            var playerTwoMilliseconds = (int)(touchController.playerTwoDateTime - touchController.startDateTime).TotalMilliseconds;

            if (playerOneMilliseconds < 0)
            {
                leftPlayerTimeTouchText.text = "--- мс";
            }
            else
            {
                leftPlayerTimeTouchText.text = playerOneMilliseconds + " мс";
            }
            
            if (playerTwoMilliseconds < 0)
            {
                rightPlayerTimeTouchText.text = "--- мс";
            }
            else
            {
                rightPlayerTimeTouchText.text = playerTwoMilliseconds  + " мс";
            }

            if (isLeftPlayerWin)
            {
                leftPlayerTextImageWinner.gameObject.SetActive(true);
            }
            else
            {
                rightPlayerTextImageWinner.gameObject.SetActive(true);
            }
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene("Shoot");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}