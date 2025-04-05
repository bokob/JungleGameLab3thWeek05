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
    Animator animator;
    NavMeshAgent nav;
    ActManager am;
    Collider c;
    SpriteRenderer sr;
<<<<<<< HEAD:Assets/ksh/Script/AI.cs

=======
    Status status;
>>>>>>> main:Assets/Ksh/Script/AI.cs
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        am = GetComponent<ActManager>();
        c = GetComponentInChildren<Collider>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
<<<<<<< HEAD:Assets/ksh/Script/AI.cs
=======
        status =GetComponentInChildren<Status>();
       if (Manager.Game!=null) Manager.Game.SpawnedList.Add(transform);
>>>>>>> main:Assets/Ksh/Script/AI.cs

        StartCoroutine(Sycle());
    }


    void Update()
    {
        if (status != null&&status.IsDead ==true)
        {
            sr.color = Color.grey;

            animator.SetFloat("spd", 0);
            //  animator.SetTrigger("DieTrigger");


            return;    
        }

        target = GetCloseEnemy(gameObject, sight.Radious);
        if (target)
        {
            animator.SetFloat("spd", 0);
            if (am.now[1] == null)
            {

                //이동
                if (Vector2.Distance(transform.position, target.transform.position) > navStopDistance)
                {

                    Vector2 fr = transform.position;
                    Vector2 to = target.transform.position;
                    Vector3 dir = to - fr; dir.Normalize();

                    transform.position += dir * speed * Time.deltaTime;

                    animator.SetFloat("spd", speed);
                }
                //방향
                sr.flipX = false;
                if (transform.position.x > target.transform.position.x)
                    sr.flipX = true;
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

    private void OnDestroy()
    {

        Manager.Game.SpawnedList.Remove(transform);
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
<<<<<<< HEAD:Assets/ksh/Script/AI.cs
            var v = cs[i].GetComponentInParent<Status>(); if (v==null) continue;//공격ㅇ         
            if (v.gameObject == gameObject) continue;
            //if (Info.isDiffer(fr, v.gameObject)==false) continue;//다른 팀
            //var v = cs[i].GetComponentInParent<Life>();
            //if (IsVisible(v.gameObject) == false) continue;
=======
            var v = cs[i].GetComponentInParent<Status>(); if (v==null) continue;//공격ㅇ     
            if (v.gameObject == gameObject) continue;


            var info = v.GetComponent<Info>();//ai는 항상info가짐 / 적이 가지면
            if (info != null) 
                if (Info.isDiffer(fr, v.gameObject) == false) 
                    continue;//다른 팀

>>>>>>> main:Assets/Ksh/Script/AI.cs

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
 * 


            //if (Info.isDiffer(fr, v.gameObject)==false) continue;//다른 팀
            //var v = cs[i].GetComponentInParent<Life>();
            //if (IsVisible(v.gameObject) == false) continue;
            var info = v.GetComponent<Info>();
            var info_target = v.GetComponent<Info>();
            if (info != null)
            {
                if (info.owner != null)//2차생성된 유닛 (소환수)
                {
                    if (v.gameObject == info.owner) continue;
                    if (info_target != null)  //소환수끼리 싸움방지
                    {
                        if (Info.isDiffer(fr, v.gameObject) == false) continue;//다른 팀
                    }
                }
            } 
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