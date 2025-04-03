using System.Collections.Generic;
using UnityEngine;

public class KnightBT : Tree
{
    static float _moveSpeed = 0.25f;        // 이동 속도
    static float _attackRange = 0.25f;  // 공격 사거리
    static public float MoveSpeed => _moveSpeed;
    static public float AttackRange => _attackRange;

    [SerializeField] float _testAttackRange = 0.2f;

    [SerializeField] Transform _target;

    [SerializeField] bool _isReadyDash;

    Status _status;

    void Start()
    {
        _status = GetComponent<Status>();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testAttackRange);
    }
}