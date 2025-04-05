using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    private float speed = 1f;         // 속도 증가
    private int damage = 10;
    private Vector2 direction;
    private Vector2 startPosition;
    private float maxDistance = 10f;
    private float knockbackForce = 0.5f;

    public Sprite[] fireballSprites;
    public float animationSpeed = 0.1f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateFireball());
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        startPosition = transform.position;
        //Debug.Log("발사 시작 위치: " + startPosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        //Debug.Log("이동 거리: " + distanceTraveled);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Status enemy = other.GetComponent<Status>();
        if (enemy != null && other.gameObject != transform.parent?.gameObject) // 위자드 제외
        {
            enemy.TakeDamage(damage);
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                Debug.Log("넉백 적용! 힘: " + knockbackForce);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator AnimateFireball()
    {
        int index = 0;
        while (true)
        {
            spriteRenderer.sprite = fireballSprites[index];
            index = (index + 1) % fireballSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}