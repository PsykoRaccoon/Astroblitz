using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class ShipHealth : MonoBehaviour
{
    public int lives = 3; 
    public TextMeshProUGUI livesText; 
    public GameObject gameOverCanvas; 
    public Animator playerAnimator; 
    public GameObject pausaCanvas;

    void Start()
    {
        lives = PlayerPrefs.GetInt("PlayerLives", 3); 
        UpdateLivesText();
        gameOverCanvas.SetActive(false); 
        Time.timeScale = 1; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            LoseLife(); 
            StartCoroutine(RestartSceneAfterDelay(2.2f)); 
        }
    }

    void LoseLife()
    {
        lives--; 
        PlayerPrefs.SetInt("PlayerLives", lives); 
        UpdateLivesText();

        Collider2D playerCollider = GetComponent<Collider2D>();

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        playerAnimator.SetTrigger("Death"); 

        if (lives <= 0)
        {
            GameOver(); 
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Lifes: " + lives; 
    }

    void GameOver()
    {
        StartCoroutine(ShowGameOverCanvas(2.0f)); 
        pausaCanvas.SetActive(false);
    }

    IEnumerator ShowGameOverCanvas(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); 
        gameOverCanvas.SetActive(true); 
        Time.timeScale = 0; 
    }


    IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void ResetLives()
    {
        PlayerPrefs.DeleteKey("PlayerLives"); 
        lives = 3; 
        UpdateLivesText();
        Time.timeScale = 1; 
    }
}
