using UnityEngine;

public class Bow : MonoBehaviour
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
