using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ImageModel buttonPlay;
    [SerializeField] private ImageModel buttonExit;
    [SerializeField] private ImageModel imageSamuraiDuel;

    private void Start()
    {
        buttonPlay.StartTweener();
        buttonExit.StartTweener();
        //imageSamuraiDuel.StartTweener();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Shoot");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
