using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    private GameObject[] _weapons;
    private PlayerAttack _playerAttack;
    private int _currentWeaponIndex = 0;

    void Start()
    {
        _weapons = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _weapons[i] = transform.GetChild(i).gameObject;
        }
        Manager.Input.transformAction += WeaponTransform;
        _playerAttack = transform.parent.GetComponent<PlayerAttack>();
    }

    public void WeaponTransform()
    {
        _weapons[_currentWeaponIndex].SetActive(false);

        if (_currentWeaponIndex == _weapons.Length - 1)
        {
            _currentWeaponIndex = 0;
        }
        else
        {
            _currentWeaponIndex++;
        }

        _weapons[_currentWeaponIndex].SetActive(true);
        _playerAttack.currentWeaponIndex = _currentWeaponIndex;
    }
}