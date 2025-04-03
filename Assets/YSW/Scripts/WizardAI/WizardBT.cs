
using System.Collections.Generic;

using BehaviorTree;

public class WizardBT : Tree
{

    public static float speed = 1f;
    public static float fovRange = 20f;
    public static float attackRange = 2f;
    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            // 텔레포트 루틴 (최우선)
            new Sequence(new List<Node>
            {
                new CheckEnemyInTeleportRange(transform),
                new TaskTeleport(transform),

            }),// 공격 루틴
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),// 추적 루틴
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),

        });

        return root;
    }
}
