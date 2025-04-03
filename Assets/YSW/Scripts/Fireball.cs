using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private float speed = 1f; // 파이어볼 속도
    private int damage = 10;  // 데미지
    private Vector2 direction;
    private Vector2 startPosition;  // 발사 시작 위치
    private float maxDistance = 10f; // 최대 거리 10유닛

    private float knockbackForce = 1f; // 넉백 힘 크기


    public Sprite[] fireballSprites; // 애니메이션용 스프라이트 배열
    public float animationSpeed = 0.1f; // 프레임 변경 속도

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateFireball()); // 애니메이션 시작
    }


    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        startPosition = transform.position; // 시작 위치 저장

        //  방향을 회전시킴 (이제 보이는 방향과 이동 방향이 동일!)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        


    }

    void Update()
    {
        //  이동 시 월드 기준으로 이동하도록 변경
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        //transform.Translate(direction * speed * Time.deltaTime);

        // 거리 체크
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject); // 최대 거리 넘으면 삭제
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyManager enemy = other.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            // 데미지 주기
            enemy.TakeHit(damage);

            // 넉백 힘 추가
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

                
            }

            
            // 파이어볼 삭제
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