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

    void Start()
    {
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
                new CheckEnemyInAttackRange_Knight(transform),
                new TaskAttack_Knight(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckDash_Knight(transform),
                new TaskDash_Knight(transform),
            }),
            new Sequence(new List<Node> 
            {
                new CheckCloseEnemy_Knight(transform),
                new TaskGoToTarget_Knight(transform),
            })
        });
        return root;
    }

    public void Die()
    {
        _spriteRenderer.color = Color.gray;                     // 회색 처리
        transform.Find("Weapon").gameObject.SetActive(false);   // 무기 비활성화
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testAttackRange);
    }
}