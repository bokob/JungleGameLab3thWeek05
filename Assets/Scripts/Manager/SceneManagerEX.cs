using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class SceneManagerEX
{
    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // sceneType으로 씬 전환
    public void SwitchScene(SceneType sceneType)
    {
        string sceneName = sceneType.ToString();
        SceneManager.LoadScene(sceneName);
    }

    // 씬 전환될 때 일어나는 이벤트
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BaseScene currentScene = GameObject.FindAnyObjectByType<BaseScene>();
        currentScene.Init();
    }

    public void Clear()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } 
}