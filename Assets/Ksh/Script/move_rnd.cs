using System.Collections;
using UnityEngine;


public class move_rnd : MonoBehaviour
{

    public float dist_max;
    public float speed;
    public Vector2 next;


    void Start()
    {
        StartCoroutine(Fwd_Rnd());
    }



    IEnumerator Fwd_Rnd()
    {
        Vector3 tar = transform.parent.position;
        tar.x += Random.RandomRange(-dist_max, dist_max);
        tar.y += Random.RandomRange(-dist_max, dist_max);
        tar.z = transform.position.z;



        for (; ; )
        {
            transform.position += (tar - transform.position).normalized * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, tar) < 1.1f)
                break;

            yield return new WaitForSeconds(Time.deltaTime);
        }



        yield return new WaitForSeconds(Random.RandomRange(next.x, next.y));
        StartCoroutine(Fwd_Rnd());
    }





}
/*
        var dist_rnd = Random.RandomRange(0.1f, dist_max);
        
        transform.Rotate(transform.forward, Random.Range(-180, 180));
        Vector3 dir = tar - transform.position;



    public void Fwd_player_Rnd(float f)
    {
        Vector3 tar = transform.position;
        tar.x += Random.RandomRange(-f, f);
        tar.y += Random.RandomRange(-f, f);

 var dest = Vector3.Distance(transform.position, tar);



        Vector3 dir = tar - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


    }
 
 */
