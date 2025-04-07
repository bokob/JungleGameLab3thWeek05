using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcess_Player : MonoBehaviour
{
    public int sight_count;
    public float sight = 1f;
    public float sight_to;
    Volume volume;
    ChromaticAberration  chromaticAberration;
    Vignette vignette;
    Status status;



    void Start()
    {
        status=GetComponent<Status>();  
        volume =Camera.main.GetComponent<Volume>();


       if (volume.profile.TryGet(out chromaticAberration))
       {
           chromaticAberration.intensity.value = 0f; // 초기 Bloom 강도 설정
       }
       if (volume.profile.TryGet(out vignette))
       {
           vignette.intensity.value = 0f; // 초기 Vignette 강도 설정
       }
    }

    
    void Update()
    {
        if (chromaticAberration != null)
        {
            if (status.HP / status.MaxHP < 0.3f)
            { chromaticAberration.intensity.value = .5f; }
            else
            { chromaticAberration.intensity.value = 0; }

        }

        if (vignette != null)
        {
            if (sight_count > 0)
                sight_to = sight;
            else
                sight_to = 0;
        }


        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, sight_to, 3*Time.deltaTime);   

    }
}
