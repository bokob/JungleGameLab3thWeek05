
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Acting : MonoBehaviour
{
    public GameObject next;
    public float next_time;
    public Vector2 next_multi;// x 개수 y 각도
    public int channel;
    public string ani;
    public float Char_trail;


    [Header("Start")]
    public bool this_pos_to;
    public float this_pos_fwd;

    public float this_rotate_Rnd;
    public bool this_rot_up;
    public bool this_parent_to_owner;


    [Header("Update")]
    public float owner_looking_target;
    public bool owner_pos_to_this;
    public float owner_SideWalk; 
    public float owner_Dash;
    public float this_looking_target;
    public bool this_Potision_to_owner;


    [Header("End")]
    public float destroy_time;
    Info info;
    Vector3 bef;
    Rigidbody2D rb;
    Animator animator;
    public UnityEvent onEnd;
    int rnd; //-1 or 1 



    void Start()
    {
        if (destroy_time > 0) Destroy(gameObject, destroy_time);
        Invoke("Next", next_time);


        info = GetComponent<Info>();
        if (info.owner) rb = info.owner.GetComponent<Rigidbody2D>();
        if (info.owner) animator = info.owner.GetComponentInChildren<Animator>();


        rnd = Random.Range(0, 2) * 2 - 1;


        if (channel > 0) info.owner.GetComponent<ActManager>().now[channel] = gameObject;
        if (this_pos_to) transform.position = info.to;
        if (this_pos_fwd > 0) transform.position += transform.up * this_pos_fwd;
        if (this_rotate_Rnd > 0) transform.Rotate(transform.forward, Random.Range(-this_rotate_Rnd, this_rotate_Rnd)); //랜덤 Y축 회전   


        if (this_rot_up)
        {
            transform.up = Vector3.up;
            if (info.owner)
                if (info.owner.transform.position.x > info.target.transform.position.x)
                    transform.right = -transform.right;
        }
        if (this_parent_to_owner) transform.parent = info.owner.transform;








        if (info.owner) animator.SetTrigger(ani);
        if (Char_trail > 0) info.owner.GetComponentInChildren<TrailSprite>().MakeTrail(Char_trail);
    }
    public void Next()
    {
        if (next == null) return;


        if (next_multi.x > 0)
        {
            Quaternion before = transform.rotation;
            transform.Rotate(transform.forward, -next_multi.x * next_multi.y / 2);
            transform.Rotate(transform.forward, -next_multi.y / 2);
            for (int i = 0; i < next_multi.x; i++)
            {
                transform.Rotate(transform.forward, next_multi.y);
                var o = Instantiate(next, transform.position, transform.rotation);
                o.GetComponent<Info>().Init(info.owner, info.to, info.target, info.act);
            }
            transform.rotation = before;

        }
        else
        {
            var o = Instantiate(next, transform.position, transform.rotation);
            o.GetComponent<Info>().Init(info.owner, info.to, info.target, info.act);
        }

    }



    void Update()
    {
        if (owner_looking_target > 0)
        {
        //    Vector3 to = info.target.transform.position;
        //    Vector3 now = info.owner.transform.position;
        //    Vector3 dir = to - now; dir.y = 0;

        //    Quaternion q = Quaternion.LookRotation(dir);
        //    info.owner.transform.rotation = Quaternion.Slerp(info.owner.transform.rotation, q, Time.deltaTime * owner_looking_target);
        }

        if (this_looking_target > 0)
        {
            Vector3 to = (info.target.transform.position - info.owner.transform.position).normalized;
            transform.up = to;// Vector2.Lerp(transform.up, to, Time.deltaTime * this_looking_target);


            //    Vector2 to = info.target.transform.position;
            //    Vector2 now = transform.position;
            //    Vector3 dir = to - now; dir.z = 0;

            //    Quaternion q = Quaternion.LookRotation(dir);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * this_looking_target);

        }

        if (owner_pos_to_this)
            info.owner.transform.position = transform.position;

        if (this_Potision_to_owner)
            transform.position = info.owner.transform.position;

        if (owner_Dash != 0)
            rb.linearVelocity = transform.up * owner_Dash;



        if(owner_SideWalk !=0)
        {
            transform.up = (info.target.transform.position -info.owner.transform.position ).normalized;         

            info.owner.transform.position += (transform.right*rnd*2  +  transform.up).normalized  
                * owner_SideWalk * Time.deltaTime;

            animator.SetFloat("spd", owner_SideWalk);
        }

        bef = transform.position;
    }

    private void OnDestroy()
    {
        if (channel > 0) info.owner.GetComponent<ActManager>().now[channel] = null;

        onEnd.Invoke();
    }

    public void FindPosRnd(float f)
    {
        Vector3 to = info.target.transform.position;
        to.x += Random.Range(-f, f);
        to.y += Random.Range(-f, f);


        transform.transform.up = (to - transform.position).normalized;
    }
    public void Set_owner_velocity(float v)
    {
        rb.linearVelocity = rb.linearVelocity.normalized*v;

    }
    public void Rotate_side(int i)
    {

        transform.up= (info.target.transform.position - info.owner.transform.position).normalized;
        transform.up = (transform.right * i *1     + transform.up*1);

    }
    public void OwnerPushSide(float v)
    {
        rb.linearVelocity = transform.right * rnd * v;
    }
    public void OwnerPushUp(float v)
    {
        rb.linearVelocity = transform.up * v;
    }

    public void RotateRnd()
    {
        if (this_rotate_Rnd > 0) transform.Rotate(transform.forward, Random.Range(-this_rotate_Rnd, this_rotate_Rnd)); //랜덤 Y축 회전   
    }

}



/*transform.rotation = Quaternion.LookRotation(to-transform.position);
 if (owner_position_to_target)
{
    var nav = owner.GetComponent<NavMeshAgent>();
    nav.enabled = false;

    me.transform.position = target.transform.position + (-target.transform.forward) * 1;
    nav.GetComponent<NavMeshAgent>().enabled = true;

} 
 */
