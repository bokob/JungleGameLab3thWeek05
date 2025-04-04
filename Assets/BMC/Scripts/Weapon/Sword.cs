using UnityEngine;

public class Sword : MonoBehaviour
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