
using System.Collections.Generic;

using BehaviorTree;
using UnityEngine;

public class WizardBT_Y : Tree
{

    static float _moveSpeed = 0.25f;        // 이동 속도
    static float _attackRange = 5f;  // 공격 사거리
    static public float MoveSpeed => _moveSpeed;
    static public float AttackRange => _attackRange;

    [SerializeField] float _testAttackRange = 5f;



    [SerializeField] Transform _target;

    Status _status;
    //protected override Node SetupTree()
    //{

    //    Node_Y root = new Selector_Y(new List<Node_Y>
    //    {
    //        // 텔레포트 루틴 (최우선)
    //        new Sequence_Y(new List<Node_Y>
    //        {
    //            new CheckEnemyInTeleportRange_Y(transform),
    //            new TaskTeleport_Y(transform),

    //        }),// 공격 루틴
    //        new Sequence_Y(new List<Node_Y>
    //        {
    //            new CheckEnemyInAttackRange_Y(transform),
    //            new TaskAttack_Y(transform),
    //        }),// 추적 루틴
    //        new Sequence_Y(new List<Node_Y>
    //        {
    //            new CheckEnemyInFOVRange_Y(transform),
    //            new TaskGoToTarget_Y(transform),
    //        }),

    //    });

    //    return root;
    //}

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
                new CheckEnemyInTeleportRange_Y(transform),
                new TaskTeleport_Y(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange_Y(transform),
                new TaskAttack_Y(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckCloseEnemy_Y(transform),
                new TaskGoToTarget_Y(transform),
            }),
        });
        return root;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testAttackRange);
    }
}
