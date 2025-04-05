using System.Collections;
using UnityEngine;

public class TrailSprite : MonoBehaviour
{
    public float time_interval;
    public float time_remaining;

    SpriteRenderer sr;
         


    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

    }

    public void MakeTrail(float time)
    {
        StartCoroutine(Sycle(time));    
    }
    IEnumerator Sycle(float time)
    {
        float duration = time; // 3초
        float elapsedTime = 0f;

        for (; ; )
        {
            GameObject newObject = new GameObject("TrailSprite");
            newObject.transform.position = transform.position;
            newObject.transform.rotation = transform.rotation;
            newObject.transform.localScale = transform.lossyScale;
           
            var sprite = newObject.AddComponent<SpriteRenderer>();
            sprite.sprite = sr.sprite;

            Color newColor = Color.gray;
            newColor.a = 0.5f;
            sprite.color = newColor;

            Destroy(newObject, time_remaining);


            yield return new WaitForSeconds(time_interval); //현제적 없다 


            elapsedTime += time_interval;
            if (elapsedTime >= duration)
                break;
        }
    }

}
