using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialForce = 300f;
    public float maxSpeed = 10f;
    public float radius = 0.25f;
    public Transform paddle;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.AddForce(new Vector2(1, 1).normalized * initialForce);
    }

    void Update()
    {
        LimitSpeed();

        // Colisión con paredes
        Vector2 pos = rb.position;
        if (pos.x < -8f + radius || pos.x > 8f - radius)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }

        if (pos.y > 4.5f - radius)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }

        if (pos.y < -5f)
        {
            rb.velocity = Vector2.zero;
            rb.position = Vector2.zero;
            rb.AddForce(new Vector2(1, 1).normalized * initialForce);
        }

        // Paddle
        CheckCircleAABBCollision(paddle);

        // Blocks
        foreach (var block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (CheckCircleAABBCollision(block.transform))
            {
                Destroy(block);
            }
        }
    }

    void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    bool CheckCircleAABBCollision(Transform target)
    {
        Vector2 circleCenter = rb.position;
        Vector2 boxCenter = target.position;
        Vector2 boxSize = target.localScale;

        Vector2 closest = new Vector2(
            Mathf.Clamp(circleCenter.x, boxCenter.x - boxSize.x / 2f, boxCenter.x + boxSize.x / 2f),
            Mathf.Clamp(circleCenter.y, boxCenter.y - boxSize.y / 2f, boxCenter.y + boxSize.y / 2f)
        );

        float distance = Vector2.Distance(circleCenter, closest);

        if (distance < radius)
        {
            Vector2 normal = (circleCenter - closest).normalized;
            rb.velocity = Vector2.Reflect(rb.velocity, normal);
            return true;
        }

        return false;
    }
}
