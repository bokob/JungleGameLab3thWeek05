using Unity.VisualScripting;
using UnityEngine;
using static Define;

// 가까운 적 확인하는 노드
public class CheckCloseEnemy : Node
{
    static int _enemyLayerMask = 1 << 6;

    Transform _transform;
    Animator _animator;

    public CheckCloseEnemy(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    //public override NodeState Evaluate()
    //{
    //    object targetObject = GetData("target");
    //    if (targetObject == null)
    //    {
    //        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, KnightBT.FovRange, _enemyLayerMask);

    //        if (colliders.Length > 0)
    //        {
    //            parent.Parent.SetData("target", colliders[0].transform);
    //            //_animator.SetBool("Walking", true);
    //            nodeState = NodeState.Success;
    //            return nodeState;
    //        }

    //        nodeState = NodeState.Failure;
    //        return nodeState;
    //    }

    //    nodeState = NodeState.Success;
    //    return nodeState;
    //}

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            // 적 중에 가장 가까운 적을 찾는다
            Transform target = null;
            Transform closeEnemy = null;
            float dist = float.MaxValue;
            foreach (Transform enemy in Manager.Instance.EnemyList)
            {
                if (Vector3.Distance(_transform.position, enemy.position) < dist)
                {
                    dist = Vector3.Distance(_transform.position, enemy.position);
                    closeEnemy = enemy;
                }
            }

            //Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, KnightBT.FovRange, _enemyLayerMask);

            //if (colliders.Length > 0)
            //{
            //    parent.Parent.SetData("target", colliders[0].transform);
            //    //_animator.SetBool("Walking", true);
            //    nodeState = NodeState.Success;
            //    return nodeState;
            //}

            //nodeState = NodeState.Failure;
            //return nodeState;

            parent.Parent.SetData("target", closeEnemy.transform);
            //_animator.SetBool("Walking", true);
            nodeState = NodeState.Success;
            return nodeState;
        }
        else // 기존에 target이 있는 경우
        {
            Debug.Log("target 새로 찾아야함");

            // 적 중에 가장 가까운 적을 찾는다
            Transform target = (Transform)targetObject;
            Transform closeEnemy = target;
            float dist = Vector3.Distance(_transform.position, target.position);
            foreach (Transform enemy in Manager.Instance.EnemyList)
            {
                if(Vector3.Distance(_transform.position, enemy.position) < dist)
                {
                    dist = Vector3.Distance(_transform.position, enemy.position);
                    closeEnemy = enemy;
                }
            }

            parent.Parent.SetData("target", closeEnemy.transform);
            //_animator.SetBool("Walking", true);
            nodeState = NodeState.Success;
            return nodeState;
        }
        //nodeState = NodeState.Success;
        //return nodeState;
    }
}