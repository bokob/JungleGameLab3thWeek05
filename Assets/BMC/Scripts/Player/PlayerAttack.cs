using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Excalibur _excalibur;
    
    void Start()
    {
        _excalibur = GetComponentInChildren<Excalibur>();
        Manager.Input.attackAction += _excalibur.Use;
    }
}