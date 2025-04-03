using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class AIBehaviorHandler : MonoBehaviour
{
    [SerializeField] private List<string> _approachActs = new List<string>(); // 접근 중 실행할 Act 이름들
    [SerializeField] private List<string> _notApproachActs = new List<string>(); // 접근 중이 아닐 때 실행할 Act 이름들

    public virtual void ExecuteBehavior(BossAI boss)
    {
        if (boss.target == null) return;

        List<string> actsToExecute = new List<string>();

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
            boss.StartPossibleAct(actsToExecute);
        }
    }

    // Inspector에서 접근 가능 (선택 사항)
    public List<string> ApproachActs => _approachActs;
    public List<string> NotApproachActs => _notApproachActs;
}