using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Status _status;

    void Start()
    {
        _status = GetComponent<Status>();
    }

    void Update()
    {
        Manager.UI.setHealthBarAction?.Invoke(_status.HP);
    }
}