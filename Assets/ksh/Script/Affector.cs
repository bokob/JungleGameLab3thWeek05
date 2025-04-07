using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affector : MonoBehaviour
{
    public float damage;
    public float push;


    public UnityEngine.Events.UnityEvent OnHit;
    Info info;
    List<GameObject> hitted = new List<GameObject>();//중복방지start

    private void Start()
    {
        info = GetComponent<Info>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Common(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Common(collision.gameObject);
    }



    public void Common(GameObject go)
    {
        var v = go.GetComponentInParent<Status>();  if (v == null) return;
        if (hitted.Contains(v.gameObject) == true) return;

        var infoTarget = go.GetComponentInParent<Info>();//ai는 항상info가짐 / 적이 가지면
        if (info !=null&& info.owner && infoTarget != null)
            if (Info.isDiffer(info.owner, v.gameObject) == false)
                return;//다른 팀




        hitted.Add(v.gameObject);


        if(info)
        v.TakeDamage(damage * info.multiply);
        else 
            v.TakeDamage(damage);  


        if (info && info.owner != null && info.owner.GetComponent<PlayerController>() != null)
        {
            if (v.IsDead == true)
            {
                if (v.gameObject.name.Contains("Knight"))
                {
                    info.owner.GetComponentInChildren<PlayerTransform>().excaliburLevel++;
                }
                else if (v.gameObject.name.Contains("Archer"))
                {
                    info.owner.GetComponentInChildren<PlayerTransform>().bowLevel++;
                }
                else if (v.gameObject.name.Contains("Wizard"))
                {
                    info.owner.GetComponentInChildren<PlayerTransform>().staffLevel++;
                }
                else if (v.gameObject.name.Contains("Orc"))
                {
                    info.owner.GetComponent<Status>().HP += 50;
                    info.owner.GetComponent<Status>().MaxHP += 50;
                }
                else if (v.gameObject.name.Contains("Necromancer"))
                {
                    // 네크로맨서 처치 시 체력 재생 효과
                    HealthRegeneration regen = info.owner.GetComponent<HealthRegeneration>();
                    if (regen != null)
                    {
                        regen.OnNecromancerKilled();
                    }
                    else
                    {
                        Debug.LogWarning("Affector: 플레이어에 HealthRegeneration 컴포넌트가 없습니다!");
                    }
                }
            }
        }
        v.GetComponent<Rigidbody2D>().linearVelocity =
            (v.gameObject.transform.position - info.owner.transform.position).normalized * push;

        OnHit.Invoke();

    }

    public void Dest() { Destroy(gameObject); }

}

/*
 * 
            if (Info.isDiffer(info.owner, go.gameObject) == false
        if (info.owner == go.gameObject)  return;//소유자 방지 

 
 
        var l = go.GetComponentInParent<Info>();  if (l == null) return;
        if (hitted.Contains(l.gameObject) == true) return;
        if (Info.isDiffer(info.owner, go.gameObject) == false) return;


 
 
 */