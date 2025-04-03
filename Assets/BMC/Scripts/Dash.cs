using UnityEngine;

public class Dash : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _dashSpeed;

    SpriteRenderer _spriteRenderer;
    Color _color;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Play(Vector2 direction)
    {
        _rb.linearVelocity = direction * _dashSpeed;
    }
}
