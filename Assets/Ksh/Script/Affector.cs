using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affector : MonoBehaviour
{
    public float damage;
    public float push;


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



    public void Common(GameObject go)
    {
        var v = go.GetComponentInParent<Status>();  if (v == null) return;
        if (hitted.Contains(v.gameObject) == true) return;
        if (info.owner == go.gameObject)  return;




        hitted.Add(v.gameObject);


        v.TakeDamage(damage);
        if(v.IsDead == true)
        {
            if (v.gameObject.name.Contains("Knight"))
            {
                info.owner.GetComponentInChildren<PlayerTransform>().excaliburLevel++;
            }
            else if (v.gameObject.name.Contains("Archer"))
            {
                info.owner.GetComponentInChildren<PlayerTransform>().bowLevel++;
                Debug.Log("Bow Level Up");
            }
            else if (v.gameObject.name.Contains("Wizard"))
            {
                info.owner.GetComponentInChildren<PlayerTransform>().staffLevel++;
            }
        }

        v.GetComponent<Rigidbody2D>().linearVelocity =
            (v.gameObject.transform.position - info.owner.transform.position).normalized * push;

        OnHit.Invoke();

    }

    public void Dest() { Destroy(gameObject); }

}

/*
 
 
        var l = go.GetComponentInParent<Info>();  if (l == null) return;
        if (hitted.Contains(l.gameObject) == true) return;
        if (Info.isDiffer(info.owner, go.gameObject) == false) return;


 
 
 */