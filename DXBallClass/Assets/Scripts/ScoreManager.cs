using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject ball;
    public GameObject paddle;
    int score = 0;
    
    public void addScore(int input)
    { 
        if (input == 1)
        {
            score = score + input;
            scoreText.text = score.ToString() + "Points";
            if (score == 23) // Win when all 23 bricks are destroyed
            {
                winText.text = "You Win!";
                ball.SetActive(false);
                
                // Stop paddle movement on win as well
                if (paddle != null)
                {
                    var paddleController = paddle.GetComponent<PaddleController>();
                    if (paddleController != null)
                    {
                        paddleController.enabled = false;
                        Debug.Log("Paddle movement disabled - You Win!");
                    }
                }
            }
        }
        else if (input == 0)
        {
            winText.text = "Game Over!";
            
            // Stop paddle movement by disabling its script
            if (paddle != null)
            {
                // Disable the paddle controller script specifically
                var paddleController = paddle.GetComponent<PaddleController>();
                if (paddleController != null)
                {
                    paddleController.enabled = false;
                    Debug.Log("Paddle movement disabled - Game Over!");
                }
                else
                {
                    Debug.LogError("PaddleController component not found on paddle!");
                }
            }
            else
            {
                Debug.LogError("Paddle reference is null!");
            }
        }
    }
}