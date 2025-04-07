using UnityEngine;

public class InGameScene : BaseScene
{
    float _roundTextTime = 2f;

    void Start()
    {
        Manager.Game.GameStart();
        StartCoroutine(Util.WaitTimeAfterPlayAction(_roundTextTime, () =>
        {
            Manager.Game.SpawnEnemy();
        }));
    }

    public override void Init()
    {
        Manager.Game.Init();
        Manager.Input.Init();
        Debug.Log("InGameScene 세팅");
    }

    void OnDestroy()
    {
        Manager.Input.Clear();
        Manager.Game.Clear();
    }
}