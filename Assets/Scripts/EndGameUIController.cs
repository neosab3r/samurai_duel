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
            
            mainPanel.SetActive(true);
            leftPlayerTimeTouchText.text = touchController.playerOneDateTime.Millisecond + " мс";
            rightPlayerTimeTouchText.text = touchController.playerTwoDateTime.Millisecond + " мс";

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