using System;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("컴포넌트")]
    Animator _anim;
    Collider2D _collider;

    [Header("능력치")]
    public float HP { get => _hp; set { _hp = value; } }
    public float MaxHP { get => _maxHP; set { _maxHP = value; } }
    public int AdditionalLife { get => _additionalLife; set { _additionalLife = value; } }
    public bool IsDead => _isDead;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] float _hp;
    [SerializeField] float _maxHP = 100f;
    [SerializeField] bool _isDead = false;
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] int _additionalLife = 0;

    public Action HitAction;
    public Action DieAction;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider2D>();
        Init();
    }

    void Init()
    {
        //_maxHP = 100f;
        _hp = _maxHP;
        _moveSpeed = 1;
    }

    // 좌우 뒤집기
    public void Flip(Transform targetTransform, float x)
    {
        Quaternion rotationY = targetTransform.rotation;
        if (x > 0)
            rotationY = Quaternion.Euler(0, 0, 0);
        else if(x < 0)
            rotationY = Quaternion.Euler(0, 180, 0);
        targetTransform.rotation = rotationY;
    }

    // 데미지 받기
    public void TakeDamage(float damage)
    {
        if(_isDead) return;
        _hp = Mathf.Clamp(_hp - damage, 0, _maxHP);
        if (_hp <=0)
        {
            Die();
        }
        else
        {
            HitAction?.Invoke();            // 플레이어만 작동
            _anim.SetTrigger("HitTrigger");
        }
    }

    void Die()
    {
        if(_additionalLife > 0)
        {
            if (_additionalLife > 5) _additionalLife = 5;
            _hp = _maxHP * _additionalLife * 0.2f;
            _additionalLife = 0;
            return;
        }
        _isDead = true;
        _collider.enabled = false;
        Manager.Game.SpawnedList.Remove(transform);

        _anim.SetTrigger("DieTrigger");
        DieAction?.Invoke();
        //_spriteRenderer.color = Color.gray;
        //_silhouette.Clear();
    }
}