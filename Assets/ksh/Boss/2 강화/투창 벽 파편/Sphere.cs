using UnityEngine;
using UnityEngine.Events;

public class Sphere : MonoBehaviour
{
    public GameObject pref;//파편

    public UnityEvent onCollide;
    public LayerMask mask;


    private void Update()
    {
        var v = Physics2D.RaycastAll(transform.position, transform.up, 0.5f, mask);
        for (int i = 0; i < v.Length; i++)
        {
            if (v[i] != null)
            {
                if(v[i].transform.GetComponent<BoxCollider2D>() ==null) 
                    continue;




                transform.up = Vector2.Reflect(transform.up,   v[i].normal);
                Debug.Log(v[i].transform.gameObject);


                onCollide.Invoke();
                Destroy(gameObject);
                break;

            }
        }




    }


    public void Common(GameObject go)
    {
        if (go.layer == 0)
        {



           // var v = GetComponentsInChildren<Collider>();//.enabled = false;
           // for (int i = 0; i < v.Length; i++)
           // { v[i].enabled = false; }



        }



      //  var l = go.GetComponentInParent<Info>(); if (l == null) return;
      //  
      //
      //  if (Info.isDiffer(info.owner, go.gameObject) == false) return;
      //
      //
      //
      //
      //  l.GetComponent<Rigidbody2D>().linearVelocity =
      //      (l.gameObject.transform.position - info.owner.transform.position).normalized * push;
      //
      //  OnHit.Invoke();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Common(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Common(collision.gameObject);
    }

    public void Dest() { Destroy(gameObject); }
}
