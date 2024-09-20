using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShipHealth : MonoBehaviour
{
    private int lives = 3; 
    public TextMeshProUGUI livesText; 
    public GameObject gameOverCanvas; 
    public Animator playerAnimator; 
    public GameObject pausaCanvas;
    public ShipShooting shipShooting;
    private Rigidbody2D rb;
    private Points pointsSystem;

    void Start()
    {
        shipShooting = GetComponent<ShipShooting>();
        rb = GetComponent<Rigidbody2D>();

        GameObject gameController = GameObject.FindWithTag("GameController");
        if (gameController != null)
        {
            pointsSystem = gameController.GetComponent<Points>();
        }

        lives = PlayerPrefs.GetInt("PlayerLives", 3); 
        UpdateLivesText();
        gameOverCanvas.SetActive(false); 
        Time.timeScale = 1; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Handheld.Vibrate();
            LoseLife(); 
            StartCoroutine(RestartSceneAfterDelay(2.2f)); 
        }
    }

    void LoseLife()
    {
        shipShooting.enabled = false;
        StopMovement();
        lives--; 
        PlayerPrefs.SetInt("PlayerLives", lives); 
        UpdateLivesText();

        Collider2D playerCollider = GetComponent<Collider2D>();

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        playerAnimator.SetTrigger("Death");

        if (pointsSystem != null)
        {
            pointsSystem.isPlayerAlive=false;
            pointsSystem.UpdateHighScore();
            pointsSystem.ResetScore();
        }

        if (lives <= 0)
        {
            GameOver(); 
        }
    }

    void StopMovement()
    {
        if (rb != null)
        {
            rb.simulated = false;
            rb.Sleep();
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Lifes: " + lives; 
    }

    void GameOver()
    {
        if (pointsSystem != null)
        {
            pointsSystem.UpdateHighScore();
        }

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
