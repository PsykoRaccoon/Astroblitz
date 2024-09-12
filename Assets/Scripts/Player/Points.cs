using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public float pointsPerSecond; 
    public TextMeshProUGUI scoreText; 

    private float score; 

    void Update()
    {
        score += pointsPerSecond * Time.deltaTime;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score); 
        }
    }
}
