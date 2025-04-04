using UnityEngine;
using static Define;

public class TaskAttack_Archer : Node
{
    Animator _anim;
    Transform _transform;
    Transform _bow;
    GameObject _arrowPrefab;

    Bow _bowAnim;

    float _attackCooldown = 2f;     // 공격 후 쿨타임 (아처는 더 빠른 공격 속도)
    float _attackCounter = 0f;      // 쿨타임 카운터

    public TaskAttack_Archer(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _bowAnim = transform.GetComponentInChildren<Bow>();
        _bow = _transform.Find("Weapon/Bow");
        if (_bow == null)
        {
            Debug.LogError("Bow 오브젝트를 Archer > Weapon 아래에 추가해주세요!");
        }

        _arrowPrefab = Resources.Load<GameObject>("Arrow");
        if (_arrowPrefab == null)
        {
            Debug.LogError("Arrow 프리팹을 Resources 폴더에서 찾을 수 없어요!");
        }
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 타겟이 죽었는지 확인
        Status targetStatus = target.GetComponent<Status>();
        if (targetStatus != null && targetStatus.IsDead)
        {
            ClearData("target"); // 죽은 타겟 지우기
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 쿨타임 체크
        if (_attackCounter > 0)
        {
            _attackCounter -= Time.deltaTime;
            //_anim.SetBool("IsMove", false); // Idle
            nodeState = NodeState.Running;
            return nodeState;
        }

        // 즉시 공격 (캐스팅 없음)
        Vector3 spawnOffset = (target.position - _bow.position).normalized * 0.5f;
        GameObject arrow = GameObject.Instantiate(_arrowPrefab, _bow.position + spawnOffset, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            _bowAnim.Use();
            Vector2 direction = (target.position - _bow.position).normalized;
            arrowScript.SetDirection(direction);
            //Debug.Log("화살 발사!");
        }

        _attackCounter = _attackCooldown; // 쿨타임 시작
        _anim.SetBool("IsMove", false);   // Idle

        nodeState = NodeState.Running;
        return nodeState;
    }
}