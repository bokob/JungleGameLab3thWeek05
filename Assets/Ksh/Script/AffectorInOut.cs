using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectorInOut : MonoBehaviour
{
    public bool sight;


    public UnityEngine.Events.UnityEvent OnHit;
    Info info;
    List<GameObject> hitted = new List<GameObject>();//중복방지start

    private void Start()
    {
        info = GetComponent<Info>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Common(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Common(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CommonOut(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CommonOut(collision.gameObject);
    }
    void CommonOut(GameObject go)
    {
        var v = go.GetComponentInParent<Status>(); if (v == null) return;


        if (hitted.Contains(v.gameObject) == true)
            hitted.Remove(v.gameObject);

            v.GetComponent<PostProcess_Player>().sight_count -=1;
    }




    public void Common(GameObject go)
    {
        var v = go.GetComponentInParent<Status>();  if (v == null) return;
        if (hitted.Contains(v.gameObject) == true) return;

        var infoTarget = go.GetComponentInParent<Info>();//ai는 항상info가짐 / 적이 가지면
        if (info !=null&& info.owner && infoTarget != null)
            if (Info.isDiffer(info.owner, v.gameObject) == false)
                return;//다른 팀






        hitted.Add(v.gameObject);

            v.GetComponent<PostProcess_Player>().sight +=1;    


    }


}

/*
 * 
            if (Info.isDiffer(info.owner, go.gameObject) == false
        if (info.owner == go.gameObject)  return;//소유자 방지 

 
 
        var l = go.GetComponentInParent<Info>();  if (l == null) return;
        if (hitted.Contains(l.gameObject) == true) return;
        if (Info.isDiffer(info.owner, go.gameObject) == false) return;


 
 
 */