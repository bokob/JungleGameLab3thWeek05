using UnityEngine;
using static Define;

// 가까운 적 확인하는 노드
public class CheckCloseEnemy : Node
{
    Transform _transform;

    public CheckCloseEnemy(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        Transform closeEnemy = null;
        float dist = float.MaxValue;
        foreach (Transform enemy in Manager.Game.SpawnedList)
        {
            if (enemy == _transform)
                continue;

            if (Vector3.Distance(_transform.position, enemy.position) < dist)
            {
                dist = Vector3.Distance(_transform.position, enemy.position);
                closeEnemy = enemy;
            }
        }

        parent.Parent.SetData("target", closeEnemy);
        nodeState = NodeState.Success;
        return nodeState;
    }
}