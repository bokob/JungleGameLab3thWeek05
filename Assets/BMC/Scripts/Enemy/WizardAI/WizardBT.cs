using System.Collections.Generic;
using UnityEngine;

public class WizardBT : Tree
{
    static float _moveSpeed = 0.25f;        // 이동 속도
    static float _attackRange = 5f;         // 공격 사거리
    static public float MoveSpeed => _moveSpeed;
    static public float AttackRange => _attackRange;

    [SerializeField] float _testAttackRange = 5f;
    SpriteRenderer _spriteRenderer;

    Canvas _castingCanvas;      // 캐스팅 바 캔버스
    Canvas _healthBar;          // 체력 바 캔버스
    Status _status;
    [SerializeField] Transform _target;

    void Start()
    {
        _status = GetComponent<Status>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _status.DieAction += Die;

        // CastingCanvas 찾기
        Transform castingCanvasTransform = transform.Find("CastingCanvas");
        if (castingCanvasTransform != null)
        {
            _castingCanvas = castingCanvasTransform.GetComponent<Canvas>();
        }

        //// HealthCanvas 찾기
        //Transform healthCanvasTransform = transform.Find("HealthBar");
        //if (healthCanvasTransform != null)
        //{
        //    //_healthBar = healthCanvasTransform.GetComponent<Canvas>();
        //}
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
                new CheckEnemyInTeleportRange_Wizard(transform),
                new TaskTeleport_Wizard(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange_Wizard(transform),
                new TaskAttack_Wizard(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckCloseEnemy_Wizard(transform),
                new TaskGoToTarget_Wizard(transform),
            }),
        });
        return root;
    }
    public void Die()
    {
        _spriteRenderer.color = Color.gray;                     // 회색 처리
        transform.Find("Weapon").gameObject.SetActive(false);   // 무기 비활성화
        //Manager.Game.NormalEnemyList.Remove(transform);         // 일반 적 리스트에서 제거

        // 캐스팅 바 숨기기
        if (_castingCanvas != null)
        {
            _castingCanvas.enabled = false;
            //Debug.Log("위자드 사망: 캐스팅 바 숨김");
        }

        // 체력 바 숨기기
        if (_healthBar != null)
        {
            _healthBar.enabled = false;
            //Debug.Log("위자드 사망: 체력 바 숨김");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _testAttackRange);
    }
}