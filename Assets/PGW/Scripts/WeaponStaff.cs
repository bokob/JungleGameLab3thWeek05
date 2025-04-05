using UnityEngine;

public class WeaponStaff : MonoBehaviour
{
    GameObject _fireballPrefab;

    void Start()
    {
        _fireballPrefab = Resources.Load<GameObject>("Fireball");
    }

    public void Use()
    {
        Instantiate(_fireballPrefab, transform.position, transform.rotation);
    }
}
