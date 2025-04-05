using System.Collections.Generic;
using UnityEngine;

public class KnightBT : Tree
{
    static float _moveSpeed = 1f;           // 이동 속도
    static float _attackRange = 1f;         // 공격 사거리
    static public float MoveSpeed => _moveSpeed;
    static public float AttackRange => _attackRange;

    [SerializeField] float _testAttackRange = 1f;

    [SerializeField] Transform _target;

    [SerializeField] bool _isReadyDash;

    Status _status;

    SpriteRenderer _spriteRenderer;
    Collider2D _collider;
    Animator _anim;



    void Start()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _status = GetComponent<Status>();
        _status.DieAction += Die;

    }

    void Update()
    {
        if (_root != null && !_status.IsDead)
        {
            _root.Evaluate();
            _target = (Transform)_root.GetData("target");
        }
    }

    protected override Node SetUpTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckDash(transform),
                new TaskDash(transform),
            }),
            new Sequence(new List<Node> 
            {
                new CheckCloseEnemy(transform),
                new TaskGoToTarget(transform),
            })
        });
        return root;
    }

    public void Die()
    {
        _spriteRenderer.color = Color.gray;                     // 회색 처리
        transform.Find("Weapon").gameObject.SetActive(false);   // 무기 비활성화
        Manager.Game.NormalEnemyList.Remove(transform);         // 일반 적 리스트에서 제거

       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testAttackRange);
    }
}