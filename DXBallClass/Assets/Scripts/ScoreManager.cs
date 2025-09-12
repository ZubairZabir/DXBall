using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject ball;
    
    int score = 0;

    public void addScore(int input)
    {
        if (input == 1)
        {
            score = score + input;
            scoreText.text = score.ToString() + " Points";
            if (score == 23) // Win when all 23 bricks are destroyed
            {
                winText.text = "You Win!";
                ball.SetActive(false);
            }
        }
        else if (input == 0)
        {
            Debug.Log("ScoreManager: Setting 'You Lose!' message");
            if (winText != null)
                winText.text = "You Lose!";
            else
                Debug.LogError("winText is null!");
            
            if (ball != null)
                ball.SetActive(false); // Ensure ball is inactive when you lose
            else
                Debug.LogError("ball reference is null!");
        }
    }
}