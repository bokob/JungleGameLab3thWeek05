using UnityEngine;

public class Staff_Wizard : MonoBehaviour
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
