using UnityEngine;

public class Scaleing : MonoBehaviour
{
    public float scale = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * scale * Time.deltaTime;

    }
}
