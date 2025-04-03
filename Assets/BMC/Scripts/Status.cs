using UnityEngine;

public class Status : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    Collider2D _collider;

    [SerializeField] float _hp;
    float _maxHP;
    bool _isDead = false;
    float _moveSpeed;
    public float HP => _hp;
    public float MaxHP => _maxHP;
    public bool IsDead => _isDead;
    public float MoveSpeed => _moveSpeed;

    SpriteRenderer _spriteRenderer;
    Silhouette _silhouette;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _silhouette = GetComponent<Silhouette>();
        Init();
    }

    void Init()
    {
        _maxHP = 10f;
        _hp = _maxHP;
    }

    // 좌우 뒤집기
    public void Flip(Transform targetTransform, float x)
    {
        float rotationY = (x >= 0) ? 0 : 180;
        targetTransform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    // 데미지 받기
    public void TakeDamage(float damage)
    {
        if(_isDead) return;

        _hp = Mathf.Clamp(_hp - damage, 0, _maxHP);
        _anim.SetTrigger("HitTrigger");
        if (_hp <=0)
        {
            Die();
        }
    }

    void Die()
    {
        _isDead = true;
        _collider.enabled = false;
        Manager.Instance.EnemyList.Remove(transform);

        _anim.SetTrigger("DieTrigger");
        _spriteRenderer.color = Color.gray;
        _silhouette.Clear();
        transform.Find("Weapon").gameObject.SetActive(false);
    }
}