using UnityEngine;

public class WeaponBow : MonoBehaviour
{
    GameObject _arrowPrefab;

    void Start()
    {
        _arrowPrefab = Resources.Load<GameObject>("Arrow");
    }

    public void Use()
    {
        Instantiate(_arrowPrefab, transform.position, transform.rotation);
        Debug.Log("화살 발사!");
    }
}
