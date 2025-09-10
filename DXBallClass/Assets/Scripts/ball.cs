using UnityEngine;
public class ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized; //(1,1)
    }

    void Update()
    {
        // NOTE: use velocity (not linearVelocity)
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.CompareTag("paddle"))
            direction.y = -direction.y;
        else if (collison.gameObject.CompareTag("brick"))
        {
            direction.y = -direction.y;
            Destroy(collison.gameObject);
        }
        else if (collison.gameObject.CompareTag("topWall"))
            direction.y = -direction.y;
        else if (collison.gameObject.CompareTag("sideWall"))
            direction.x = -direction.x;
        else if (collison.gameObject.CompareTag("bottomWall"))
            Debug.Log("Game over");
    }
}