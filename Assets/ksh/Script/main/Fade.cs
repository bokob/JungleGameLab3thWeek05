using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;


public class Fade : MonoBehaviour
{
    public float time;
    public float mid;

     void OnEnable()
    {

       StartCoroutine( On_Off());
        
    }

    IEnumerator On_Off()
    {
        Off();

        yield return new WaitForSeconds(mid);

        On();
    }


    public void On() { StartCoroutine(On2()); }
    public void Off() { StartCoroutine(Off2()); }

    IEnumerator On2()
    {
        float ing = 0f; // 누적 경과 시간
        CanvasRenderer cr = gameObject.GetComponent<CanvasRenderer>();
        while (ing <= time)
        {
            cr.SetAlpha(1-( ing / time));// 1~0

            ing += Time.deltaTime;
            yield return null;
        }
        gameObject.active = false;
    }
    IEnumerator Off2()
    {
        float ing = 0f; // 누적 경과 시간
        CanvasRenderer cr = gameObject.GetComponent<CanvasRenderer>();
        while (ing <= time)
        {
            //Debug.Log(cr.GetAlpha());
            cr.SetAlpha( ing / time); //0~1

            ing += Time.deltaTime;
            yield return null;
        }
    }

}
