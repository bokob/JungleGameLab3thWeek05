using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Use()
    {
        _anim.SetTrigger("AttackTrigger");
    }
}