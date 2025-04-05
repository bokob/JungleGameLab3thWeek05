using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum Team {Plyaer,Enemy }
  

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5;
    public Act[] acts;
    public Act actNow;

    [Space(30)]
    public GameObject light;
    public float lightSpeed ;
    Quaternion light_temp;

    [Space(30)]
    public Transform CameraPivot;
    public Transform CameraHandle;
    public float mouseSensitivity = 1000; //마우스감도
    float MouseY;
    float MouseX;

    void Start()
    {
        acts = GetComponentsInChildren<Act>();

    }

    // Update is called once per frame
    void Update()
    {
        //이동
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(x,0, z).normalized * Time.deltaTime * moveSpeed);


        light.transform.rotation = Quaternion.Lerp(light.transform.rotation, light_temp, lightSpeed * Time.deltaTime);
        light_temp = Camera.main.transform.rotation;




        if (Input.GetMouseButton(0))
        {
            var to = Camera.main.ScreenToWorldPoint(Input.mousePosition); to.z = 0;
        }
    }
    private void LateUpdate()//부드러워짐 
    {


        MouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp를 통해 최소값 최대값을 넘지 않도록함


        CameraPivot.rotation = Quaternion.Euler(MouseY, MouseX, 0f);// 각 축을 한꺼번에 계산                                                                  
        Camera.main.transform.SetPositionAndRotation(CameraHandle.position, CameraHandle.rotation);


    }

}








/*
 
    public void GameEnd()
    {
        Time.timeScale = 0;
    }

    //다시시작
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
        if (Input.GetMouseButton(1))
        {
            var to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            to.z = 0;

            acts[1].Start_Act(gameObject, transform.position, to);

        }


        ////무기교체 
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log(SceneManager.GetActiveScene().buildIndex);
        //    if (SceneManager.GetActiveScene().buildIndex != 0)
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetAllScenes().Length - 2)
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
 
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 tp = transform.position;
        //
        //transform.position += new Vector3(h * move_speed * Time.deltaTime, 0);
        //transform.position += new Vector3(0, v * move_speed * Time.deltaTime, 0);
*/