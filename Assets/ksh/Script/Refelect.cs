using UnityEngine;

public class Refelect : MonoBehaviour
{
    public LayerMask mask;


    private void Update()
    {
        var v = Physics2D.RaycastAll(transform.position, transform.up, 0.5f, mask);
        for (int i = 0; i < v.Length; i++)
        {
            if (v[i] != null)
            {
                if (v[i].collider.isTrigger== false)
                 transform.up = Vector2.Reflect(transform.up, v[i].normal);
            }
        }
    }
}
