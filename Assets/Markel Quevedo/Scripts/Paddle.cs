using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(input, 0, 0) * speed * Time.deltaTime;
        transform.position += move;

        // Limita el movimiento dentro de la pantalla
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -7f, 7f),
            transform.position.y,
            0f
        );
    }
}
