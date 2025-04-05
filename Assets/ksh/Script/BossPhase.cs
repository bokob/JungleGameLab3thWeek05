using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public List<GameObject> list = new();

<<<<<<< HEAD

=======
    int now;
    Status status;
    void Start()
    {
        status = GetComponent<Status>();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].active = false;
        }
        list[now].active = true;
        GetComponentInParent<ActManager>().ReFindActs();
    }
>>>>>>> main


    void Update()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.Alpha1))
=======
        if (now == 0)
        {
            if (status != null && status.HP / status.MaxHP < 0.7f)
                now++;
        }
        if (now == 1)
        {
            if (status != null && status.HP / status.MaxHP < 0.3f)
                now++;
        }



        if (list[now] !=null && list[now].active == false)
>>>>>>> main
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].active = false;
            }
<<<<<<< HEAD
            list[0].active = true;
            GetComponentInParent<ActManager>().ReFindActs();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].active = false;
            }
            list[1].active = true;
            GetComponentInParent<ActManager>().ReFindActs();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].active = false;
            }
            list[2].active = true;
            GetComponentInParent<ActManager>().ReFindActs();

        }
    }
}
=======
            list[now].active = true;
            GetComponentInParent<ActManager>().ReFindActs();
        }

    }
}
>>>>>>> main
