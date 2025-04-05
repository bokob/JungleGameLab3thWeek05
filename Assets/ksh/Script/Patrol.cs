using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float CheckDistance = 2;
    public float R = 2;
    public LayerMask CheckLayer;




    void Update()
    {
        bool ischek = Physics2D.CircleCast(transform.position, R, transform.up, CheckDistance, CheckLayer);

        if (ischek)
            transform.Rotate(transform.forward, Random.RandomRange(-180, 180));//  direction *= -1;

    }
}
