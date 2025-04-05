
using UnityEngine;
using UnityEngine.UIElements;


public class GunAim : MonoBehaviour
{
    public GameObject gun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        to.z = 0;


        //transform.rotation = Quaternion.LookRotation(to - transform.position);
        Vector3 dir = to - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);



        if (to.x < transform.position.x)
        {
            if(transform.localScale.y>0)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.y < 0)
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        }
    }
}
