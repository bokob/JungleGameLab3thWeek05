using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcess_Player : MonoBehaviour
{
    public Volume volume;
    ChromaticAberration  chromaticAberration;
    Vignette vignette;
    Status status;



    void Start()
    {
        status=GetComponent<Status>();  
        volume =Camera.main.GetComponent<Volume>();


        if (volume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.intensity.value = 1f; // 초기 Bloom 강도 설정
        }
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0.3f; // 초기 Vignette 강도 설정
        }
    }

    
    void Update()
    {
        if (chromaticAberration != null)
            chromaticAberration.intensity.value = (status.MaxHP-status.HP)/1.5f / status.MaxHP;
    
    }
}
