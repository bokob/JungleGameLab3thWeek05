using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFwd : MonoBehaviour
{
    public float speed;
    public float accel;


    void Update()
    {
        //ÀüÁø
        transform.position += transform.up * Time.deltaTime * speed;
       
        speed += accel * Time.deltaTime;
        if (speed < 0) speed = 0;
    }
}
