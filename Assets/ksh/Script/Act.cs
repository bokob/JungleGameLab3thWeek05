using UnityEngine;

public class Act : MonoBehaviour
{
    public GameObject next;
    public int channel;
    public int str_index;

    [Space(30)]
    public float charge_str;
    public float charge_max;
    float charge_ing;

    [Space(30)]
    public float range_max; //최대 
    public float range_min; //최th 
    public bool require_target;


    ActManager acts;
    GameObject str;

    void Start()
    {
        acts = GetComponentInParent<ActManager>();

        
        charge_ing = charge_str;
    }

    void Update()
    {
        //================== 충전중 ==================
        if (charge_ing < charge_max)
        {
            charge_ing += Time.deltaTime;

            //과충전
            if (charge_ing >= charge_max)
                charge_ing = charge_max;
        }
    }

    public bool Check_Condition(Vector3 to, GameObject target = null)
    {
        if (charge_ing < charge_max)
            return false;


        float dist = Vector3.Distance(transform.position, to);
        if (dist > range_max)
            return false;

        if(dist < range_min)
            return false;


        if (require_target)
            if (target == null)
                return false;

        if (channel > 0)
            if (acts.now[channel] != null)
                return false;

        return true;
    }

    public void Try_Act(GameObject owner,Vector3 to, GameObject target = null)
    {
        if (Check_Condition(to, target) == false)
            return;


        charge_ing = Random.Range(0, charge_max / 5);



        Vector3 fr = owner.transform.position;
        GameObject o = Instantiate(next, fr, Quaternion.LookRotation(to - fr));
        o.transform.up = (to - fr).normalized;
        var info = o.GetComponent<Info>();
        info.Init(owner, to, target, this);
    } 
    
    //private void OnDrawGizmos()
    //{
    //    UnityEditor.Handles.color = Color.green;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, range_max);
    //}
}

/*
 

        if (channel > 0)
            acts.now.Insert(channel, this);
 
    [Space(30)]
    public float cast_time; //총 집중시간 
    public float str; //
    public string ani;

 
 */
