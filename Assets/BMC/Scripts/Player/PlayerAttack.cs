using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    Excalibur _excalibur;
    WeaponBow _bow;
    WeaponStaff _staff;

    void Start()
    {
        _excalibur = GetComponentInChildren<Excalibur>(true);
        _bow = GetComponentInChildren<WeaponBow>(true);
        _staff = GetComponentInChildren<WeaponStaff>(true);
        Manager.Input.attackAction += Attack;
    }

    public void Attack()
    {
        switch(currentWeaponIndex)
        {
            case 0:
                _excalibur.Use();
                break;
            case 1:
                _bow.Use();
                break;
            case 2:
                _staff.Use();
                break;
        }
    }
}