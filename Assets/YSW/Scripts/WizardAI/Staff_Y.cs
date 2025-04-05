using UnityEngine;

public class Staff_Y : MonoBehaviour
{
    Animator _anim;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Use()
    {
        _anim.SetTrigger("AttackTrigger");
    }
}
