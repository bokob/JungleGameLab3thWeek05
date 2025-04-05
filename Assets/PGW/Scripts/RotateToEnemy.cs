using UnityEngine;

public class RotateToEnemy : MonoBehaviour
{
    public float rotate = 1f;

    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + rotate);
    }
}
