using System;
using UnityEngine;

public class UIManager
{
    #region Title 씬
    public Action toggleTitleBtns;
    public Action toggleHelpPanel;
    #endregion

    #region InGame 씬
    public Action<float> setHealthBarAction;
    public Action toggleGameClearCanvasAction;
    public Action toggleGameOverCanvasAction;
    public Action toggleRoundStartCanvasAction;
    #endregion

    public void Init()
    {
        Debug.Log("UI");
    }
}