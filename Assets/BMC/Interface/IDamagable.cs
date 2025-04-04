using UnityEngine;

// 피해 입고 죽을 수 있는 인터페이스
public interface IDamagable
{
    public void TakeDamage(float damage);
    public void Die();
}