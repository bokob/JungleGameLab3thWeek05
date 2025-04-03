using UnityEngine;

public class RotateToEnemy : MonoBehaviour
{
    Info info;
    public float speed;
    public float accel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info = transform.parent.GetComponent<Info>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = info.target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += transform.up * Time.deltaTime * speed;

        speed += accel * Time.deltaTime;
        if (speed < 0) speed = 0;
    }
}
