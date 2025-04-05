using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public List<GameObject> list = new();

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


    void Update()
    {
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
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].active = false;
            }
            list[now].active = true;
            GetComponentInParent<ActManager>().ReFindActs();
        }

    }
}