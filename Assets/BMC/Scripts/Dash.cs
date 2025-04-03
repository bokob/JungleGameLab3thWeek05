using UnityEngine;

public class Dash
{
    float _dashSpeed = 3f;

    public void Play(Rigidbody2D rb, Vector2 direction)
    {
        rb.linearVelocity = direction * _dashSpeed;
        Debug.Log("대시");
    }
}