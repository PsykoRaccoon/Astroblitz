using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShipHealth : MonoBehaviour
{
    public int lives = 3; // Cantidad de vidas iniciales del jugador
    public TextMeshProUGUI livesText; // Referencia al componente de texto que mostrar� las vidas
    public GameObject gameOverCanvas; // Referencia al Canvas de Game Over
    public Animator playerAnimator; // Referencia al Animator del jugador

    void Start()
    {
        // Reiniciar las vidas a 3 cada vez que se inicie el juego
        lives = 3;
        PlayerPrefs.SetInt("PlayerLives", lives); // Guardar el n�mero de vidas iniciales
        UpdateLivesText();
        gameOverCanvas.SetActive(false); // Asegurarse de que el Canvas de Game Over est� oculto al principio
        Time.timeScale = 1; // Asegurarse de que el juego se est� ejecutando a velocidad normal al inicio
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el jugador ha chocado con un objeto que tenga el tag "Asteroid"
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            LoseLife(); // Llamar a la funci�n para perder una vida
        }
    }

    void LoseLife()
    {
        lives--; // Reducir la cantidad de vidas
        PlayerPrefs.SetInt("PlayerLives", lives); // Guardar el n�mero de vidas
        UpdateLivesText();

        if (lives <= 0)
        {
            GameOver(); // Llamar a la funci�n de Game Over
        }
        else
        {
            // Iniciar la animaci�n de muerte si hay una
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Death"); // Asumiendo que tienes un trigger llamado "Death"
            }
            StartCoroutine(RestartSceneAfterDelay(3f)); // Esperar 3 segundos antes de reiniciar
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Vidas: " + lives; // Actualizar el texto con la cantidad de vidas restantes
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true); // Mostrar el Canvas de Game Over
        Time.timeScale = 0; // Pausar el juego
    }

    IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Esperar la cantidad de segundos especificada
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }

    // M�todo para restablecer las vidas (por ejemplo, cuando se reinicie el juego)
    public void ResetLives()
    {
        PlayerPrefs.DeleteKey("PlayerLives"); // Eliminar las vidas guardadas
        lives = 3; // Reiniciar las vidas a 3
        UpdateLivesText();
        Time.timeScale = 1; // Reiniciar la velocidad del juego
    }
}
