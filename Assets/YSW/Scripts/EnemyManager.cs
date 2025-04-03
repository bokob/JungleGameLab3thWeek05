using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int health = 20;

    private void Awake()
    {
        health = 30;
    }

    public bool TakeHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            return true; // 죽음
        }
        return false; // 안 죽음
    }

   
}
