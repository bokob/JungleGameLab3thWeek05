using UnityEngine;

public class Enemy : MonoBehaviour
{



    public void Flip(Transform targetTransform, float x)
    {
        if(x >= 0)
        {
            targetTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}