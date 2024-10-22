using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject storeCanvas;
    public GameObject optionsCanvas;
    public float buttonDelay;

    private void Start()
    {
        Time.timeScale=1;
        ShowMenu();
    }

    public void Play()
    {
        StartCoroutine(DelayedPlay());
    }

    public void ShowMenu()
    {
        StartCoroutine(DelayedShowMenu());
    }

    public void ShowStore()
    {
        StartCoroutine(DelayedShowStore());
    }

    public void ShowOptions()
    {
        StartCoroutine(DelayedShowOptions());
    }

    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(buttonDelay);
        SceneManager.LoadScene("Game");
        ResetLives();
    }

    private IEnumerator DelayedShowMenu()
    {
        yield return new WaitForSeconds(buttonDelay);
        menuCanvas.SetActive(true);
        storeCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
    }

    private IEnumerator DelayedShowStore()
    {
        yield return new WaitForSeconds(buttonDelay);
        menuCanvas.SetActive(false);
        storeCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    private IEnumerator DelayedShowOptions()
    {
        yield return new WaitForSeconds(buttonDelay);
        menuCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    private void ResetLives()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
    }
}
