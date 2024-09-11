using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen;

    public GameObject pauseBtn;

    private bool isPaused = false;

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            pauseBtn.SetActive(true);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            pauseBtn.SetActive(false);
            isPaused = true;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
