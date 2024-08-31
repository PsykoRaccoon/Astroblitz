using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen;

    private bool isPaused = false;

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            isPaused = true;
        }
    }
}
