using UnityEngine;

public class ResourceManager
{
    // Resources/ 경로에 있는 것 로드
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // Resources/Prefabs/ 경로에 있는 프리팹 인스턴스화
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject go = Load<GameObject>($"Prefabs/{path}");
        if (go == null)
        {
            return null;
        }
        return Object.Instantiate(go, parent);
    }
}