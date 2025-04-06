using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class AIBehaviorHandler : MonoBehaviour
{
    [SerializeField] private List<Act> _approachActs = new List<Act>(); // 접근 중 실행할 Act 이름들
    [SerializeField] private List<Act> _notApproachActs = new List<Act>(); // 접근 중이 아닐 때 실행할 Act 이름들
    [SerializeField] private Act _evasionAct;
    [SerializeField] private BossAI boss;
    private float detectionRadius = 3f; // 감지 범위 3

    void Update()
    {
        // Affector 태그나 컴포넌트를 가진 투사체 감지
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D obj in nearbyObjects)
        {
            // 투사체가 Affector 스크립트를 가지고 있는지 확인
            if (obj.GetComponent<Affector>() != null)
            {
                // 투사체가 범위 내에 있으면 회피 함수 호출
                Evasion();
                break; // 한 번 회피하면 더 이상 체크하지 않음 (필요에 따라 수정 가능)
            }
        }
    }

    public virtual void ExecuteBehavior()
    {
        if (boss.target == null) return;

        List<Act> actsToExecute = new List<Act>();

        if (boss.IsTargetApproaching)
        {
            // 접근 중일 때 모든 _approachActs 실행
            actsToExecute.AddRange(_approachActs);
        }
        else // 접근 중이 아닐 때
        {
            foreach (var actName in _notApproachActs)
            {
                actsToExecute.Add(actName);
            }
        }

        // 조건 만족하는 모든 Act 실행
        if (actsToExecute.Count > 0)
        {
            //boss.StartPossibleAct(actsToExecute);
        }
    }

    void Evasion()
    {
        boss.StartAct(_evasionAct);
    }

    // 디버깅용: 감지 범위 시각화 (에디터에서만 보임)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}