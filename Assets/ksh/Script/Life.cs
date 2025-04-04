using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float now=100;
    public float max=100;

    public UnityEngine.Events.UnityEvent OnDeath;
    public UnityEngine.Events.UnityEvent OnHit;

    public void Hit(float daage)
    {
        now -= daage;

        if (now <= 0)
            OnDeath.Invoke();
    }
    public void Dest() { Destroy(gameObject); }
}
