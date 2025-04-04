using UnityEngine;
using static Define;

public class TaskRetreatAndAttack_Archer : Node
{
    Transform _transform;
    Animator _anim;
    Transform _bow;
    GameObject _arrowPrefab;
    Status _enemy; // Flip을 위해 추가
    float _attackCooldown = 2f;
    float _attackCounter = 0f;

    Bow _bowAnim;


    public TaskRetreatAndAttack_Archer(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _bow = _transform.Find("Weapon/Bow");
        _bowAnim = transform.GetComponentInChildren<Bow>();
        _arrowPrefab = Resources.Load<GameObject>("Arrow");
        _enemy = transform.GetComponent<Status>(); // Status 컴포넌트 가져오기
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

        // 현재 거리 확인
        float distance = Vector3.Distance(_transform.position, target.position);
        if (distance < ArcherBT_Archer.MinRange) // 최소 거리보다 가까우면 뒤로 이동
        {
            Vector2 dir = (_transform.position - target.position).normalized; // 타겟 반대 방향
           _enemy.Flip(_transform, -dir.x); // 방향 반전 (적이 뒤에 있으므로)

            // 타겟 반대 방향으로 이동
            _anim.SetBool("IsMove", true);
            Vector3 retreatPosition = _transform.position + (Vector3)dir * (ArcherBT_Archer.AttackRange - distance);
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                retreatPosition,
                ArcherBT_Archer.MoveSpeed * Time.deltaTime);
        }


        // 공격 가능 시 화살 발사
        if (_attackCounter <= 0)
        {
            _bowAnim.Use();
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
