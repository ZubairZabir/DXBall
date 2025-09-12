using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PaddleController : MonoBehaviour
{
    public float displacement = 0.25f;
    public Transform leftWall;   // drag sideWall (left)
    public Transform rightWall;  // drag sideWall (right)

    Rigidbody2D rb;
    float halfWidth;
    float minX, maxX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // paddle visual width from collider bounds
        halfWidth = GetComponent<BoxCollider2D>().bounds.extents.x;
    }

    void Start()
    {
        // compute clamp limits from wall centers and widths
        float leftEdge  = leftWall.GetComponent<BoxCollider2D>().bounds.max.x;
        float rightEdge = rightWall.GetComponent<BoxCollider2D>().bounds.min.x;

        // keep full paddle inside
        minX = leftEdge  + halfWidth;
        maxX = rightEdge - halfWidth;
    }

    void Update()
    {
        Vector2 pos = rb.position;

        if (Input.GetKey(KeyCode.RightArrow)) pos.x += displacement;
        if (Input.GetKey(KeyCode.LeftArrow))  pos.x -= displacement;

        // clamp to walls
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        rb.MovePosition(pos);
    }
}