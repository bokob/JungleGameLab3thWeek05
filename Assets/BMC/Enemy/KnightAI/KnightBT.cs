using System.Collections.Generic;
using UnityEngine;

public class KnightBT : Tree
{
    [SerializeField] Transform[] wayPoints;

    static float _speed = 1f;
    static float _fovRange = 6f;
    static float _attackRange = 0.15f;
    static public float Speed => _speed;
    static public float FovRange => _fovRange;
    static public float AttackRange => _attackRange;

    [SerializeField] float _testRange = 0.75f;

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
                new CheckCloseEnemy(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, wayPoints),
        });
        return root;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testRange);
    }
}