using UnityEngine;

public class Arrow : MonoBehaviour
{
    float _speed = 8f;         // 속도 증가
    int _damage = 10;
    Vector2 _direction;
    Vector2 _startPosition;
    float _maxDistance = 10f;
    float _knockbackForce = 2.5f;

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;
        _startPosition = transform.position;
        //Debug.Log("발사 시작 위치: " + startPosition);
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += (Vector3)_direction * _speed * Time.deltaTime;
        float distanceTraveled = Vector2.Distance(_startPosition, transform.position);
        //Debug.Log("이동 거리: " + distanceTraveled);
        if (distanceTraveled >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Status enemy = other.GetComponent<Status>();
        if (enemy != null && other.gameObject != transform.parent?.gameObject) 
        {
            
            enemy.TakeDamage(_damage);
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)

            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);
                //Debug.Log("넉백 적용! 힘: " + knockbackForce);
            }
            Destroy(gameObject);
        }
    }
}