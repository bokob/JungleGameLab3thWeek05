
using System.Collections.Generic;

using BehaviorTree;

public class WizardBT_Y : Tree_Y
{

    public static float speed = 1f;
    public static float fovRange = 20f;
    public static float attackRange = 2f;
    protected override Node_Y SetupTree()
    {

        Node_Y root = new Selector_Y(new List<Node_Y>
        {
            // 텔레포트 루틴 (최우선)
            new Sequence_Y(new List<Node_Y>
            {
                new CheckEnemyInTeleportRange_Y(transform),
                new TaskTeleport_Y(transform),

            }),// 공격 루틴
            new Sequence_Y(new List<Node_Y>
            {
                new CheckEnemyInAttackRange_Y(transform),
                new TaskAttack_Y(transform),
            }),// 추적 루틴
            new Sequence_Y(new List<Node_Y>
            {
                new CheckEnemyInFOVRange_Y(transform),
                new TaskGoToTarget_Y(transform),
            }),

        });

        return root;
    }
}
