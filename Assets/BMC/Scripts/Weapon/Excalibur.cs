using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

// 플레이어 전용 검
public class Excalibur : MonoBehaviour
{
    Animator _anim;
    private int swordLevelMax = 5;
    bool isAttacking = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void Use()
    {
        var to = Camera.main.ScreenToWorldPoint(Input.mousePosition); to.z = 0;
        int swordlevel = GetComponentInParent<PlayerTransform>().excaliburLevel;
        if (swordlevel > swordLevelMax)
            swordlevel = swordLevelMax;
        Act act = transform.GetChild(swordlevel - 1).gameObject.GetComponent<Act>();
        if (act != null && act.Check_Condition(to, null))
        {
            act.Try_Act(transform.GetComponentInParent<Status>().gameObject, to);
            _anim.SetTrigger("AttackTrigger");
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{   
    //    // 플레이어면 종료
    //    if (transform.root == collision.transform)
    //        return;

    //    if (collision.gameObject.TryGetComponent<Status>(out Status status))
    //    {
    //        Vector2 dir = (collision.transform.position - transform.position).normalized;
    //        collision.transform.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
    //        // 적에게 데미지 주기
    //        status.TakeDamage(10f);
    //        if (status.IsDead == true)
    //        {
    //            if (status.gameObject.name.Contains("Knight"))
    //            {
    //                transform.GetComponentInParent<PlayerTransform>().excaliburLevel++;
    //            }
    //            else if (status.gameObject.name.Contains("Archer"))
    //            {
    //                transform.GetComponentInParent<PlayerTransform>().bowLevel++;
    //                Debug.Log("Bow Level Up");
    //            }
    //            else if (status.gameObject.name.Contains("Wizard"))
    //            {
    //                transform.GetComponentInParent<PlayerTransform>().staffLevel++;
    //            }
    //        }
    //    }
    //}
}