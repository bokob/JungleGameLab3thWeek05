using UnityEngine;


public enum Filter { Unit, ActObj }

public class Info : MonoBehaviour
{
    public int team;


    //1�� 2�� ������

    [Space(30)]
    public GameObject owner;
    public GameObject target;
    public Vector3 to;
    public Act act;

    public void Init(GameObject _owner, Vector3 _to, GameObject _target, Act _act ) 
    {
        owner = _owner;
        to = _to;
        target = _target;
        act = _act;
        if (_owner) team = _owner.GetComponent<Info>().team;
    }

    public static bool isDiffer(GameObject t1, GameObject t2)
    {
        var v1 = t1.GetComponentInParent<Info>();
        var v2 = t2.GetComponentInParent<Info>();

        if (v1 == null || v2 == null) 
            return false;

        if (v1.team == v2.team)
            return false;

        return true;
    }
}
