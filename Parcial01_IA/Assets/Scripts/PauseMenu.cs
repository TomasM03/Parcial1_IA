using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject StopButton;
    [SerializeField] private GameObject StopMenu;
    private bool GameStoped = false;
    public string Scene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStoped)
            {
                Resume();
            }
            else
            {
                StopGame();
            }
        }
    }
    public void StopGame()
    {
        GameStoped = true;
        Time.timeScale = 0f;
        StopButton.SetActive(false);
        StopMenu.SetActive(true);
    }

    public void Resume()
    {
        GameStoped = false;
        Time.timeScale = 1f;
        StopButton.SetActive(true);
        StopMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        GameStoped = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        GameStoped = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(Scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
