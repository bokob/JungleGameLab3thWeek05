using UnityEngine;
using System.Collections.Generic;

public class BossAI : MonoBehaviour
{
    private float _sightRadius = 2000f; // 시야 반경
    private float _sightAngle = 360f; // 시야 각도
    private ActManager _actManager;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Status _status;
    public GameObject target;
    public float navStopDistance = 5f;
    public float speed = 5f;

    // 적 이동 방향 체크 관련 변수
    private Vector2 _lastTargetPosition;
    private float _checkInterval = 0.5f;
    private bool _isTargetApproaching;
    private bool _isCheckingTargetMovement;
    private GameObject _lastTarget;
    private float _movementThreshold = 0.1f;

    [SerializeField] private AIBehaviorHandler _behaviorHandler; // 행동 핸들러 참조

    private void Start()
    {
        _actManager = GetComponent<ActManager>();
        if (_behaviorHandler == null)
        {
            _behaviorHandler = GetComponent<AIBehaviorHandler>();
        }
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _status = GetComponent<Status>();
    }

    private void Update()
    {
        if (_status != null && _status.IsDead == true)
        {
            _spriteRenderer.color = Color.grey;

            _animator.SetFloat("spd", 0);
            //  animator.SetTrigger("DieTrigger");


            return;
        }

        GameObject newTarget = GetCloseEnemy(gameObject, _sightRadius);

        if (newTarget != _lastTarget)
        {
            target = newTarget;
            _lastTarget = newTarget;
            _isTargetApproaching = false;
            _isCheckingTargetMovement = false;
            if (target != null)
            {
                _lastTargetPosition = target.transform.position;
            }
        }

        if (target != null)
        {
            if (!_isCheckingTargetMovement)
            {
                StartCoroutine(CheckTargetMovement());
            }
            _animator.SetFloat("spd", 0);
            if (_actManager.now[1] == null)
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance > navStopDistance)
                {
                    Vector2 fr = transform.position;
                    Vector2 to = target.transform.position;
                    Vector3 dir = to - fr; dir.Normalize();
                    transform.position += dir * speed * Time.deltaTime;
                    _animator.SetFloat("spd", speed);
                }

                //방향
                _spriteRenderer.flipX = false;
                if (transform.position.x > target.transform.position.x)
                    _spriteRenderer.flipX = true;
            }

            // 행동 핸들러 호출
            if (_behaviorHandler != null)
            {
                _behaviorHandler.ExecuteBehavior();
            }
        }
        else
        {
            _isCheckingTargetMovement = false;
        }
    }

    private System.Collections.IEnumerator CheckTargetMovement()
    {
        _isCheckingTargetMovement = true;

        while (target != null)
        {
            yield return new WaitForSeconds(_checkInterval);

            if (target == null) break;

            Vector2 currentTargetPosition = target.transform.position;
            float lastDistance = Vector2.Distance(transform.position, _lastTargetPosition);
            float currentDistance = Vector2.Distance(transform.position, currentTargetPosition);
            float distanceChange = lastDistance - currentDistance;

            if (Mathf.Abs(distanceChange) > _movementThreshold)
            {
                _isTargetApproaching = distanceChange > 0;
                Debug.Log(_isTargetApproaching ? "적 접근 중" : "적 후퇴 중");
            }
            else
            {
                _isTargetApproaching = false;
                Debug.Log("적 정지 또는 미세 움직임");
            }

            _lastTargetPosition = currentTargetPosition;
        }

        _isCheckingTargetMovement = false;
    }

    // Act 실행 함수
    public void StartAct(Act actName)
    {
        if (target == null) return;

        foreach(var a in _actManager.possess)
        {
            if (a != null && a.gameObject.name.Contains(actName.gameObject.name))
            {
                Act act = a;
                if (act == null)
                {
                    return;
                }
                if (act != null && act.Check_Condition(target.transform.position, target))
                {
                    act.Try_Act(gameObject, target.transform.position, target);
                    return;
                }
            }
        }
    }

    public void StartPossibleAct(List<Act> acts)
    {
        if (target == null) return;


        List<Act> temp = new List<Act>();
        foreach (var a in _actManager.possess)
        {
            foreach (var actName in acts)
            {
                if (a != null && a.gameObject.name.Contains(actName.gameObject.name))
                {
                    Act act = a;
                    if (act == null)
                    {
                        return;
                    }
                    if (act != null && act.Check_Condition(target.transform.position, target))
                    {
                        temp.Add(act);
                    }
                }
            }
        }

        //랜덤실행          
        if (temp.Count > 0)
            temp[Random.Range(0, temp.Count - 1)].Try_Act(gameObject, target.transform.position, target);
    }

    public GameObject GetCloseEnemy(GameObject fr, float r)
    {
        return GetClosestByList(fr, GetEnemybyRange(fr, r));
    }

    List<GameObject> GetEnemybyRange(GameObject fr, float r)
    {
        //적탐색
        Collider2D[] cs = Physics2D.OverlapCircleAll(fr.transform.position, r);
        List<GameObject> o = new List<GameObject>();
        for (int i = 0; i < cs.Length; i++)
        {
            if (cs[i] == null) continue;
            var v = cs[i].GetComponentInParent<Status>(); if (v == null) continue;//공격ㅇ         
            if (v.gameObject == gameObject) continue;

            var info = v.GetComponent<Info>();//ai는 항상info가짐 / 적이 가지면
            if (info != null)
                if (Info.isDiffer(fr, v.gameObject) == false)
                    continue;//다른 팀

            if (o.Contains(v.gameObject) == false)
                o.Add(v.gameObject);
        }

        return o;
    }

    GameObject GetClosestByList(GameObject fr, List<GameObject> list)
    {
        List<GameObject> gos = list;
        float min = Mathf.Infinity;
        GameObject close = null;
        Vector3 now = fr.transform.position;

        for (int i = 0; i < gos.Count; i++)
        {
            float dist = (gos[i].transform.position - now).sqrMagnitude;
            if (dist < min)
            {
                min = dist;
                close = gos[i];
            }
        }
        return close;
    }

    // 접근성 제공 (BehaviorHandler에서 사용)
    public bool IsTargetApproaching => _isTargetApproaching;
    public float DistanceChange => Vector2.Distance(transform.position, _lastTargetPosition) - Vector2.Distance(transform.position, target.transform.position);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sightRadius);
    }
#endif
}