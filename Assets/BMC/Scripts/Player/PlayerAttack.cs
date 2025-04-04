using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Sword _sword;
    
    void Start()
    {
        _sword = GetComponentInChildren<Sword>();
        Manager.Input.attackAction += _sword.Use;
    }
}