using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aazz0200_Spawn : MonoBehaviour
{
    public List< GameObject> pref;
    public float range = 30;
    public bool isY;

    public void Spawn()
    {
        //생성위치 
        Vector3 p = transform.position;

        if (isY)
            p.y += Random.RandomRange(-range, range);
        else
            p.x += Random.RandomRange(-range, range);



        //플레이어 바라보기
        Vector3 dir = FindObjectOfType<Controller>().gameObject.transform.position - p;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        GameObject go = Instantiate(pref[Random.RandomRange(0, pref.Count)]
            , p, Quaternion.AngleAxis(angle - 90, Vector3.forward));

    }


    private void OnDrawGizmos()
    {

        // Gizmos.color = Color.green;       
        //Gizmos.DrawWireSphere(transform.position, radius);



        Vector3 fr = transform.position;
        Vector3 to = transform.position;
        if (isY)
        {
            fr.y -= range;
            to.y += range;

            Gizmos.DrawLine(fr, to);
        }
        else
        {
            fr.x -= range;
            to.x += range;

            Gizmos.DrawLine(fr, to);
        }
    }
}

/*
 
 
      Vector3 p = transform.position;
        p.x += Random.RandomRange(-10, 010);
        p.y += Random.RandomRange(-10, 010);

        Vector3 d = p - transform.position; d.Normalize();
        Vector3 p2 = transform.position + d * radius;
 
 
 
 
 
 */