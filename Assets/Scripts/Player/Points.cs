using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public float pointsPerSecond;
    public TextMeshProUGUI scoreText; 

    private float score;
    public bool isPlayerAlive = true;

    private float highScore; 
    public TextMeshProUGUI highScoreText; 

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            score += pointsPerSecond * Time.deltaTime;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + Mathf.FloorToInt(score);
            }
        }
    }

    public void AddPoints(float points)
    {
        score += points;
    }

    public float GetCurrentScore()
    {
        return score;
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
