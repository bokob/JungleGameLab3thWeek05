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
        var l = go.gameObject.GetComponentInParent<Info>();
        if (l == null) return;

        if (hitted.Contains(l.gameObject) == true) return;



        if (Info.isDiffer(info.owner, go.gameObject))
        {
            hitted.Add(l.gameObject);

            l.GetComponent<Rigidbody2D>().linearVelocity =
                (l.gameObject.transform.position - info.owner.transform.position).normalized * push;


            OnHit.Invoke();
        }
    }

    public void Dest() { Destroy(gameObject); }

}
