using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aazz0200_Chain : MonoBehaviour
{
    public GameObject follow { get; set; }
    public float distance { get; set; }



    void Update()
    {
        if (follow == null)
        {
            Destroy(gameObject);
        }
        else

        {
            transform.up = follow.transform.position - transform.position;
            if (Vector3.Distance(follow.transform.position, transform.position) > distance)
            {
                Vector3 dir = follow.transform.position - transform.position;
                transform.position = follow.transform.position - dir.normalized * distance;

            }
        }
    }
}
