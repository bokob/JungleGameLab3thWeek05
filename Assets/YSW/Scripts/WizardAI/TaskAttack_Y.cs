using UnityEngine;
using static Define;

public class TaskAttack_Y : Node
{
    Animator _anim;
    Transform _transform;
    Transform _staff;
    GameObject _fireballPrefab;

    Staff_Y _staffY;

    float _attackCooldown = 5f;
    float _attackCounter = 0f;

    public TaskAttack_Y(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _staffY = transform.GetComponentInChildren<Staff_Y>();
        _staff = _transform.Find("Weapon/Staff");
        if (_staff == null)
        {
            Debug.LogError("Staff 오브젝트를 Wizard > Weapon 아래에 추가해주세요!");
        }

        _fireballPrefab = Resources.Load<GameObject>("Fireball");
        if (_fireballPrefab == null)
        {
            Debug.LogError("Fireball 프리팹을 Resources 폴더에서 찾을 수 없어요!");
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

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackCooldown)
        {
            _anim.SetBool("IsMove", false);


            _attackCounter = 0f;
            Vector3 spawnOffset = (target.position - _staff.position).normalized * 0.5f;
            GameObject fireball = GameObject.Instantiate(_fireballPrefab, _staff.position + spawnOffset, Quaternion.identity);
            
            Fireball fbScript = fireball.GetComponent<Fireball>();
            if (fbScript != null)
            {
                
                Vector2 direction = (target.position - _staff.position).normalized;
                
                fbScript.SetDirection(direction);
                Debug.Log("파이어볼 발사!");
                _staffY.Use();


            }
            
        }
        _anim.SetBool("IsMove", false);
        nodeState = NodeState.Running;
        return nodeState;
    }
}