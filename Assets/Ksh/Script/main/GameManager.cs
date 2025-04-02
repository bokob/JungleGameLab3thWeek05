using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float playtime;
    public float EndTime = 150;
    public Slider ui;
    public GameObject Victory;


    void Update()
    {
        playtime += Time.deltaTime;


        ui.value = playtime / EndTime;


        if(playtime > EndTime)
        { Victory.active = true;
            Time.timeScale = 0;
        }


       // ui.text = "Wave  " +(int) (playtime / WaveTime) + " / 5";
    }
}
