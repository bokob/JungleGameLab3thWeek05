using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public List<Act> possess  = new (); //
    public List<GameObject> now = new(); //진행중인 act 

    private void Start()
    {
        possess = GetComponentsInChildren<Act>().ToList();

        for (int i = 0; i < 5; i++)
        {
            now.Add(null);
        }
    }

    public void ReFindActs() { possess = GetComponentsInChildren<Act>().ToList(); }

}
