using UnityEngine;

public class InGameScene : BaseScene
{
    public override void Init()
    {
        Manager.Game.Init();
        Manager.Input.Init();

        Debug.Log("InGameScene 세팅");
        Manager.Game.GameStart();
    }

    void OnDestroy()
    {
        Manager.Input.Clear();
        Manager.Game.Clear();
    }
}