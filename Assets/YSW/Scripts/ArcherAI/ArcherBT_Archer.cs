using System.Collections.Generic;
using ArcherAI;
using BehaviorTree;
using UnityEngine;

public class ArcherBT_Archer : Tree
{
    static float _moveSpeed = 5f;        // 이동 속도 (아처는 조금 빠르게 설정)
    static float _attackRange = 7f;        // 공격 사거리 (아처는 더 긴 사거리)
    static public float MoveSpeed => _moveSpeed;
    static public float AttackRange => _attackRange;

    static float _minRange = 3f; // 최소 거리
    static public float MinRange => _minRange;

    [SerializeField] float _testAttackRange = 7f;

    [SerializeField] Transform _target;
    SpriteRenderer _spriteRenderer;

    Status _status;

    void Start()
    {
        _status = GetComponent<Status>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            new CheckEnemyTooClose_Archer(transform),
            new TaskRetreatAndAttack_Archer(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange_Archer(transform),
                new TaskAttack_Archer(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckCloseEnemy_Archer(transform),
                new TaskGoToTarget_Archer(transform),
            }),
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