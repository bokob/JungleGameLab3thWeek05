using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }
    [SerializeField] Vector2 _pointerPosition;
    Status _status;

    void Start()
    {
        _status = GetComponent<Status>();
    }

    void Update()
    {
        _pointerPosition = Manager.Input.PointerMoveInput;
        Vector2 dir = (_pointerPosition - (Vector2)transform.position).normalized;
        transform.right = dir;

        Vector2 scale = transform.localScale;
        scale.y = (dir.x < 0) ? -1 : 1;
        transform.localScale = scale;
    }
}