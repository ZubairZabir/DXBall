using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject ball;
    public GameObject paddle;
    public string nextLevelName = "Level 2"; // Name of the next scene to load
    public float delayBeforeNextLevel = 2f; // Delay in seconds before loading next level
    int score = 0;
    
    public void addScore(int input)
    { 
        if (input == 1)
        {
            score = score + input;
            scoreText.text = score.ToString() + "Points";
            if (score == 23) // Win when all 23 bricks are destroyed
            {
                winText.text = "You Win! Loading Next Level...";
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
                
                // Start coroutine to load next level after delay
                StartCoroutine(LoadNextLevelAfterDelay());
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
    
    IEnumerator LoadNextLevelAfterDelay()
    {
        Debug.Log($"Loading next level '{nextLevelName}' in {delayBeforeNextLevel} seconds...");
        yield return new WaitForSeconds(delayBeforeNextLevel);
        
        // Check if the scene exists in build settings
        if (Application.CanStreamedLevelBeLoaded(nextLevelName))
        {
            Debug.Log($"Loading scene: {nextLevelName}");
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError($"Scene '{nextLevelName}' not found! Make sure it's added to Build Settings.");
            winText.text = "Level Complete! (Next level not found)";
        }
    }
}