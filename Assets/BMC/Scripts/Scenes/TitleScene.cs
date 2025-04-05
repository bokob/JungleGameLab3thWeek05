using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Init()
    {
        Manager.UI.Init();
        Manager.Sound.Init();
        Manager.Data.Init();
        Debug.Log("TitleScene 세팅");
    }
}