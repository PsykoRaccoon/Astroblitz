using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public ShipHealth shipHealth; 

    public void Restart()
    {
        if (shipHealth != null)
        {
            shipHealth.ResetLives(); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
