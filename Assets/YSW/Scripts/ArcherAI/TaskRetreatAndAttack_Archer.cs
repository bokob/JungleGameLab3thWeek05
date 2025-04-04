using UnityEngine;
using static Define;

public class TaskRetreatAndAttack_Archer : Node
{
    Transform _transform;
    Transform _bow;
    GameObject _arrowPrefab;
    float _attackCooldown = 2f;
    float _attackCounter = 0f;

    public TaskRetreatAndAttack_Archer(Transform transform)
    {
        _transform = transform;
        _bow = _transform.Find("Weapon/Bow");
        _arrowPrefab = Resources.Load<GameObject>("Arrow");
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 쿨타임 체크
        if (_attackCounter > 0)
        {
            _attackCounter -= Time.deltaTime;
        }

        // 뒤로 이동
        Vector2 dir = (_transform.position - target.position).normalized; // 타겟 반대 방향
        _transform.position = Vector3.MoveTowards(
            _transform.position,
            _transform.position + (Vector3)dir,
            ArcherBT_Archer.MoveSpeed * Time.deltaTime);

        // 공격 가능 시 화살 발사
        if (_attackCounter <= 0)
        {
            Vector3 spawnOffset = (target.position - _bow.position).normalized * 0.5f;
            GameObject arrow = GameObject.Instantiate(_arrowPrefab, _bow.position + spawnOffset, Quaternion.identity);
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                Vector2 direction = (target.position - _bow.position).normalized;
                arrowScript.SetDirection(direction);
            }
            _attackCounter = _attackCooldown;
        }

        nodeState = NodeState.Running;
        return nodeState;
    }
}
