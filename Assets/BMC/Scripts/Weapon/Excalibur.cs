using System.Collections;
using UnityEngine;

// 플레이어 전용 검
public class Excalibur : MonoBehaviour
{
    Animator _anim;
    bool isAttacking = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void Use()
    {
        _anim.SetTrigger("AttackTrigger");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        // 플레이어면 종료
        if (transform.root == collision.transform)
            return;

        if (collision.gameObject.TryGetComponent<Status>(out Status status))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
            // 적에게 데미지 주기
            status.TakeDamage(10f);
        }
    }
}