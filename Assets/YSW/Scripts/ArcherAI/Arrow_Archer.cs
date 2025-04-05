using UnityEngine;

namespace ArcherAI
{
    public class Arrow_Archer : MonoBehaviour
    {
        private float speed = 7f;
        private int damage = 10;
        private Vector2 direction;
        private float maxDistance = 10f;
        private Vector2 startPosition;
        private float knockbackForce = 3f;

        void Start()
        {
            startPosition = transform.position;
        }

        public void SetDirection(Vector2 dir)
        {
            direction = dir.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void Update()
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
            float distanceTraveled = Vector2.Distance(startPosition, transform.position);
            if (distanceTraveled >= maxDistance)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Status enemy = other.GetComponent<Status>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
                Destroy(gameObject);
            }
        }
    }
}