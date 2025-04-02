using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aazz0200_Camera : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public Vector3 offset;
    public GameObject player;




    void LateUpdate()
    {
        Vector3 to = player.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, to, Time.deltaTime * cameraSpeed);



    }
}

