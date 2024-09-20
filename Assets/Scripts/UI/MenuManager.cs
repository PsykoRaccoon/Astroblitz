using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject storeCanvas;
    public GameObject optionsCanvas;

    private void Start()
    {
        ShowMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
        Handheld.Vibrate();
        ResetLives();
    }

    public void ShowMenu()
    {
        menuCanvas.SetActive(true);
        storeCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
    }

    public void ShowStore()
    {
        menuCanvas.SetActive(false);
        storeCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void ShowOptions()
    {
        menuCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    private void ResetLives()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
    }

}
