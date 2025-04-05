using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class Proj : MonoBehaviour
{
    public GameObject target;

    public float speed;
    public float dist_max;
    public float break2=3;

    public Vector3 all;
    public Coroutine c_accel;

    bool isFly;


    void Start()
    {
        target = GetComponent<Info>().target;
        c_accel = StartCoroutine(Accel());
        isFly = true;
    }


    void Update()
    {
        transform.position += all * Time.deltaTime;


        if (target == null)
            target = GetCloseEnemy(gameObject, 99);


        //거리 멀어짐  
        if (isFly == true)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= dist_max)
            {
                isFly = false;
                if (c_accel != null) { StopCoroutine(c_accel); c_accel = null; }
                StartCoroutine(Break(new Vector3(all.x, all.y, all.z)));
                all = Vector3.zero;

                c_accel = StartCoroutine(Accel());
            }
        }
        else
        {
            if (Vector3.Distance(target.transform.position, transform.position) < dist_max)
                isFly = true;
        }
    }

  
    void  OnDisable() 
    {
        StopAllCoroutines();
        all = Vector3.zero;
    }

    
    IEnumerator Break(Vector3 o)
    {
        for (; ; )
        {
            o = Vector3.Lerp(o, default, break2 * Time.deltaTime);
            transform.position += o *  Time.deltaTime;

            if (o.magnitude < 0.1f)
                break;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator Accel()
    {
        for (; ; )
        {
            Vector3 dir2 = target.transform.position - transform.position;
            all += dir2.normalized * speed * Time.deltaTime;       

            yield return new WaitForSeconds(Time.deltaTime);
        }
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
            var v = cs[i].GetComponentInParent<Status>(); if (v == null) continue;//           
            if (v.gameObject == gameObject) continue;


            if (v.GetComponent<Info>() != null)  //이거사슬 vs 그림자소환수
           if (Info.isDiffer(fr, v.gameObject) == false) continue;//다른 팀
                continue;


            //var v = cs[i].GetComponentInParent<Life>();
            //if (Info.isDiffer(fr, v.gameObject) == false) continue;//다른 팀
            //if (IsVisible(v.gameObject) == false) continue;

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

   
}



/*
   if (c_accel != null)
                //StopCoroutine(c_accel); c_accel = null;
                //StartCoroutine(Break(new Vector3(all.x, all.y, all.z)));
                //all = Vector3.zero;
                //
                //c_accel = StartCoroutine(Accel());
         all += (p.transform.position - transform.position).normalized * speed 
            * Time.deltaTime;
 */
//  all = all.normalized * max;
// var v = Vector3.Distance(p.transform.position, transform.position);

//transform.rotation = Quaternion.Slerp(transform.rotation, 
//    Quaternion.LookRotation( p.transform.position - transform.position), speed * Time.deltaTime);