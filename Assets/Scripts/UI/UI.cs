using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public ShipHealth shipHealth; // Referencia al script ShipHealth

    public void Restart()
    {
        if (shipHealth != null)
        {
            shipHealth.ResetLives(); // Resetea las vidas antes de recargar la escena
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
