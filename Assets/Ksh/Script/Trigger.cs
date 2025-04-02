using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{

    public UnityEngine.Events.UnityEvent OnEnter;
    // public UnityEngine.Events.UnityEvent OnColEnter;
    //public UnityEngine.Events.UnityEvent OnEixt;
    // public LayerMask CheckLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Controller>() != null)
            OnEnter.Invoke();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Controller>() != null)
            OnEnter.Invoke();
    }

    //하위충돌체 상위스크립트O
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Controller>() != null)
            OnEnter.Invoke();
    }




    public void StageNext()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetAllScenes().Length - 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void SaveBefore()// 현 맵 리셋
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
