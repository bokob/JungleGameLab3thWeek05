using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class Sight {
        public float Radious;
        public float Angle;

        public Sight(int Radious, int Angle) 
        {
            this.Radious = Radious;
            this.Angle = Angle;
        }
    public static void DrawFieldOfView(Vector3 position, Vector3 forward, Sight view)
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, view.Radious);

      // UnityEditor.Handles.color = Color.red;
      // UnityEditor.Handles.DrawWireArc(position, Vector3.forward, forward, view.Angle / 2, view.Radious - 0.1f);
      // UnityEditor.Handles.DrawWireArc(position, Vector3.forward, forward, -view.Angle / 2, view.Radious - 0.1f);
      //
      // UnityEditor.Handles.color = new Color(1, 0, 0, 0.1f);
      // UnityEditor.Handles.DrawSolidArc(position, Vector3.forward, forward, view.Angle / 2, view.Radious - 0.2f);
      // UnityEditor.Handles.DrawSolidArc(position, Vector3.forward, forward, -view.Angle / 2, view.Radious - 0.2f);
#endif
    }
}
public class AI : MonoBehaviour
{
    public GameObject target;
    public Sight sight = new Sight(20, 100);
    public float navStopDistance;
    public float sycle_time=0.2f;

    public float speed;

    NavMeshAgent nav;
    ActManager am;
    Collider c;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        am = GetComponent<ActManager>();
        c = GetComponentInChildren<Collider>();

        StartCoroutine(Sycle());
    }


    void Update()
    {
        target = GetCloseEnemy(gameObject, sight.Radious);
        if (target)
        {

            if (am.now[1] == null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > navStopDistance)
                { //이동
                    Vector2 fr = transform.position;
                    Vector2 to = target.transform.position;
                    Vector3 dir = to - fr; dir.Normalize();

                    transform.position += dir * speed * Time.deltaTime;
                }
            }


            StartPossibleAct();
        }

    }

    IEnumerator Sycle()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(sycle_time); //현제적 없다 


        }
    }


    public void StartPossibleAct()
    {
        if (target == null) return;


        List<Act> temp = new List<Act>();
        foreach (var i in am.possess)
        {
            if (i && i.Check_Condition(target.transform.position, target))
                temp.Add(i);
        }

        //랜덤실행          
        if (temp.Count > 0)
           temp[Random.Range(0, temp.Count - 1)].Try_Act(gameObject,target.transform.position, target);
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
            var v = cs[i].GetComponentInParent<Info>();
            if (v==null) continue;//공격ㅇ
            if (v.gameObject == gameObject) continue;
            if (Info.isDiffer(fr, v.gameObject)==false) continue;//다른 팀

            //var v = cs[i].GetComponentInParent<Life>();

            //
            //if (IsVisible(v.gameObject) == false) continue;

            if (o.Contains(v.gameObject)==false)
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

        for (int i = 0; i < gos.Count; i++)    //Enemy
        {
            float dist = (gos[i].transform.position - now).sqrMagnitude; 
            if (dist < min)//더 가까운 애 발견
            {
                min = dist;
                close = gos[i];
            }
        }
        return close;
    }

    public bool IsVisible(GameObject Target)
    {
        Vector3 fr = c.bounds.center;
        Vector3 to = Target.GetComponentInChildren<Collider>().bounds.center;
        Vector3 dir = (to - fr).normalized;
        float dist = Vector3.Distance(fr, to);


        //거리
        if (dist > sight.Radious)
            return false;


        //각도
        if (Vector3.Angle(transform.forward, dir) > sight.Angle / 2)        
            return false;



        var hit = Physics.RaycastAll(fr, dir, dist);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject)
                if (hit[i].transform.gameObject.layer == 0) //defalt
                { return false;  }
        }


        return true;
    }
 
}

/*
 *          if (target == null)
            {
                //새로탐색
                target = GetCloseEnemy(gameObject, sight.Radious);

                //새로운적 없다 
                if (target == null)
                {              

                }
            }
            //현제적 있다 
            else
            {

                //있다가 없어지면 그쪽으로이동
               // if (GetCloseEnemy(gameObject, sight.Radious) == null)
                if(Vector3.Distance(target.transform.position,transform.position)>sight.Radious)
                {
                    //if (nav.enabled)
                    //    nav.SetDestination(target.transform.position);

                    target = null;
                }
            }  //주변순찰 
                    if (nav.remainingDistance < nav.stoppingDistance + 0.1f)
                    {
                        Vector3 pos = transform.position;
                        pos.x += Random.Range(-patrol_radius, patrol_radius);
                        pos.z += Random.Range(-patrol_radius, patrol_radius);

                        if (nav.enabled)
                            nav.SetDestination(pos);
                    }
    if (sycle == false) return;
      //  Debug.Log(GetCloseEnemy(gameObject, sight.Radious));



        //현제적 없다 
        if (target == null)
        {
            //새로탐색
            target = GetCloseEnemy(gameObject, sight.Radious);

            //새로운적 없다 
            if (target == null)
            {
                //주변순찰 
                if (nav.remainingDistance < nav.stoppingDistance + 0.2f)
                {
                    Vector3 pos = transform.position;
                    pos.x += Random.Range(-patrol_radius, patrol_radius);
                    pos.z += Random.Range(-patrol_radius, patrol_radius);

                    if (nav.enabled)
                        nav.SetDestination(pos);
                }
            }
        }
        //현제적 있다 
        else
        {

            //있다가 없어지면 그쪽으로이동
            if (GetCloseEnemy(gameObject, sight.Radious) == null)
            {
                if (nav.enabled)
                    nav.SetDestination(target.transform.position);

                target = null;
            }
        }

        StartPossibleAct();
 
 */