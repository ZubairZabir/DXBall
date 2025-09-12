using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class ball : MonoBehaviour
{
    public float speed = 6f;
    public Vector2 direction = new Vector2(0.8f, -0.6f);
    public int brickCount = 0;
    public ScoreManager score;

    Rigidbody2D rb;
    CircleCollider2D cc;
    float radius;
    const float eps = 0.01f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        radius = cc.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
        
        // Ensure direction is properly set
        if (direction == Vector2.zero)
            direction = new Vector2(0.8f, -0.6f);
        direction = direction.normalized;
        
        Debug.Log($"Direction set to: {direction}");
    }

    void Start()
    {
        // Find ScoreManager
        GameObject logicObject = GameObject.FindGameObjectWithTag("logic");
        if (logicObject != null)
        {
            score = logicObject.GetComponent<ScoreManager>();
            if (score != null)
                Debug.Log("ScoreManager found and connected!");
            else
                Debug.LogError("ScoreManager component not found on logic GameObject!");
        }
        else
        {
            Debug.LogError("No GameObject with 'logic' tag found!");
        }
    }

    void Update()
    {
        // Keep constant speed
        rb.linearVelocity = direction * speed;
        
        // Debug - remove this line later
        Debug.Log($"Ball velocity: {rb.linearVelocity}, Direction: {direction}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("topWall"))
        {
            // Snap ball below the topWall to prevent escaping
            var bounds = collision.bounds;
            var pos = transform.position;
            pos.y = bounds.min.y - radius - eps;
            transform.position = pos;
            
            // Force downward direction
            direction.y = -Mathf.Abs(direction.y);
        }
        else if (collision.gameObject.CompareTag("sideWall"))
        {
            direction.x = -direction.x;
        }
        else if (collision.gameObject.CompareTag("brick"))
        {
            direction.y = -direction.y;
            Destroy(collision.gameObject);
            brickCount = brickCount + 1;
            
            // Debug score system
            if (score != null)
            {
                score.addScore(1);
                Debug.Log("Score increased! Bricks destroyed: " + brickCount);
            }
            else
            {
                Debug.LogError("Score manager is null!");
            }
        }
        else if (collision.gameObject.CompareTag("bottomWall"))
        {
            Debug.Log("Game over - Ball hit bottomWall!");
            if (score != null)
            {
                Debug.Log("Calling score.addScore(0) for lose condition");
                score.addScore(0);  // Call ScoreManager first
            }
            else
            {
                Debug.LogError("Score manager is null when hitting bottomWall!");
            }
            gameObject.SetActive(false); // Then deactivate ball
        }
    }
}