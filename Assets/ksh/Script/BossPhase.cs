using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public List<GameObject> list = new();




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].active = false;
            }
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
